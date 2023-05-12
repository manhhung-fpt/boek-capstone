using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boek.Core.Constants;
using Boek.Core.Entities;
using Boek.Infrastructure.Requests.Levels;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.Levels;
using Boek.Repository.Interfaces;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Boek.Service.Utils;
using Microsoft.EntityFrameworkCore;

namespace Boek.Service.Services
{
    public class LevelService : ILevelService
    {
        #region Fields and constructor
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public LevelService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Gets
        public LevelViewModel GetLevelById(int id, bool WithCustomers = false)
        {
            var _level = _unitOfWork.Levels.Get(id);
            if (_level == null)
            {
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.LEVEL_ID
                });
            }
            return GetResponse(_level, WithCustomers);
        }

        public BaseResponsePagingModel<LevelViewModel> GetLevels(LevelRequestModel filter, PagingModel paging, bool WithCustomers = false)
        {
            var _filter = _mapper.Map<LevelViewModel>(filter);
            var result =
                _unitOfWork.Levels.Get()
                    .ProjectTo<LevelViewModel>(_mapper.ConfigurationProvider)
                    .DynamicFilter(_filter)
                    .PagingQueryable(paging.Page, paging.Size, PageConstant.LimitPaging, PageConstant.LimitPaging);
            var list = new List<LevelViewModel>();
            list = result.Item2.OrderBy(l => l.ConditionalPoint).ToList();
            if (WithCustomers)
                list.ForEach(l => l = GetResponseDetail(l));
            else
                list.ForEach(l =>
                {
                    l.Customers.Clear();
                    l = GetResponseDetail(l);
                });
            return new BaseResponsePagingModel<LevelViewModel>()
            {
                Metadata =
                    new PagingMetadata()
                    {
                        Page = paging.Page,
                        Size = paging.Size,
                        Total = result.Item1
                    },
                Data = list
            };
        }
        #endregion

        #region Create, Update, and Convert
        public LevelViewModel CreateLevel(CreateLevelRequestModel createLevel)
        {
            var _level = _mapper.Map<Level>(createLevel);
            CheckLevel(_level);
            _unitOfWork.Levels.Create(_level);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                        ErrorMessageConstants.INSERT,
                        ErrorMessageConstants.LEVEL,
                        MessageConstants.MESSAGE_FAILED
                    });
            _level = _unitOfWork.Levels
                    .Get(a => a.Name.ToLower().Equals(_level.Name.ToLower()))
                    .SingleOrDefault();
            return GetResponse(_level);
        }

        public LevelViewModel UpdateLevel(UpdateLevelRequestModel updateLevel)
        {
            CheckLevel(_mapper.Map<Level>(updateLevel), false);
            // var _level = _unitOfWork.Levels.Get(l => l.Id.Equals(updateLevel.Id))
            //     .Include(l => l.Customers)
            //     .SingleOrDefault();
            var _level = _unitOfWork.Levels.Get(updateLevel.Id);
            _mapper.Map(updateLevel, _level);
            _unitOfWork.Levels.Update(_level);
            if (!_unitOfWork.Save())
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                        ErrorMessageConstants.UPDATE,
                        ErrorMessageConstants.LEVEL.ToLower(),
                        MessageConstants.MESSAGE_FAILED
                    });
            return GetResponse(_level);
        }

        public LevelViewModel ConvertCustomerLevelByNewLevel(int OldLevelId, int NewLevelId, bool DisableOldLevelStatus = false)
        {
            var _oldLevel = _unitOfWork.Levels
                .Get(l => l.Id.Equals(OldLevelId))
                .Include(l => l.Customers)
                .SingleOrDefault();
            var _newLevel = _unitOfWork.Levels.Get(NewLevelId);
            if (_oldLevel == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.LEVEL_OLD_ID
                });
            if (_newLevel == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.LEVEL_NEW_ID
                });
            if (!(bool)_newLevel.Status)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[]
                {
                        ErrorMessageConstants.CONVERT,
                        ErrorMessageConstants.LEVEL_NEW_ID,
                        MessageConstants.MESSAGE_FAILED,
                        "vì",
                        ErrorMessageConstants.LEVEL_STATUS.ToLower(),
                        MessageConstants.MESSAGE_INVALID
                });
            if (_oldLevel.Customers.Any())
            {
                var _customers = _oldLevel.Customers.ToList();
                _customers.ForEach(c => c.LevelId = NewLevelId);
                _unitOfWork.Customers.UpdateRange(_customers);
                if (!_unitOfWork.Save())
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                        ErrorMessageConstants.CONVERT,
                        ErrorMessageConstants.LEVEL_NEW_ID,
                        MessageConstants.MESSAGE_FAILED
                    });
            }
            var result = _unitOfWork.Levels
                .Get(l => l.Id.Equals(NewLevelId))
                .ProjectTo<LevelViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefault();
            if (result == null)
                BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.LEVEL_NEW_ID
                });
            if (DisableOldLevelStatus)
            {
                _oldLevel.Customers = null;
                _oldLevel.Status = false;
                _unitOfWork.Levels.Update(_oldLevel);
                if (!_unitOfWork.Save())
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                        ErrorMessageConstants.UPDATE,
                        ErrorMessageConstants.LEVEL_STATUS,
                        MessageConstants.MESSAGE_FAILED
                    });
            }
            return GetResponseDetail(result);
        }
        #endregion

        #region Utils

        private LevelViewModel GetResponse(Level level, bool WithCustomers = true)
        {
            var _response =
                _unitOfWork
                    .Levels
                    .Get(l => l.Id.Equals(level.Id))
                    .ProjectTo<LevelViewModel>(_mapper.ConfigurationProvider)
                    .SingleOrDefault();
            if (!WithCustomers) _response.Customers.Clear();
            return GetResponseDetail(_response) ?? new LevelViewModel();
        }

        private LevelViewModel GetResponseDetail(LevelViewModel _response)
        {
            if (_response != null)
            {
                _response = ServiceUtils.GetResponseDetail(_response);
                if (_response.Customers.Any())
                {
                    _response
                        .Customers
                        .ForEach(c => c.User = ServiceUtils.GetResponseDetail(c.User));
                }
            }
            return _response;
        }

        private Level CheckDuplicatedLevelName(string name) => _unitOfWork.Levels.CheckDuplicatedLevelName(name);
        private Level CheckDuplicatedLevelConditionalPoint(int? conditionalPoint) => _unitOfWork.Levels.CheckDuplicatedLevelConditionalPoint(conditionalPoint);

        private void CheckLevel(Level level, bool IsCreate = true)
        {
            if (IsCreate)
            {
                if (CheckDuplicatedLevelName(level.Name) != null)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                        MessageConstants.MESSAGE_DUPLICATED_INFO,
                        ErrorMessageConstants.LEVEL_NAME
                    });
                if (CheckDuplicatedLevelConditionalPoint(level.ConditionalPoint) != null)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                        MessageConstants.MESSAGE_DUPLICATED_INFO,
                        ErrorMessageConstants.LEVEL_CONDITIONAL_POINT
                    });
            }
            else
            {
                var _level = _unitOfWork.Levels.Get(l => l.Id.Equals(level.Id))
                .Include(l => l.Customers)
                .SingleOrDefault();
                if (_level == null)
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.NotFound), new string[]
                    {
                        ErrorMessageConstants.NOT_FOUND,
                        ErrorMessageConstants.AUTHOR_ID
                    });
                var _name = CheckDuplicatedLevelName(level.Name);
                if (_name != null)
                {
                    if (!_name.Id.Equals(level.Id))
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                            MessageConstants.MESSAGE_DUPLICATED_INFO,
                            ErrorMessageConstants.LEVEL_NAME
                        });
                }
                var _conditionalPoint = CheckDuplicatedLevelConditionalPoint(level.ConditionalPoint);
                if (_conditionalPoint != null)
                {
                    if (!_conditionalPoint.Id.Equals(level.Id))
                        BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                            MessageConstants.MESSAGE_DUPLICATED_INFO,
                            ErrorMessageConstants.LEVEL_CONDITIONAL_POINT
                        });
                }
                if (!(bool)level.Status &&
                !((bool)level.Status).Equals(_level.Status) &&
                _level.Customers.Any())
                    BoekErrorMessage.ShowErrorMessage(((int)HttpStatusCode.Conflict), new string[] {
                        ErrorMessageConstants.UPDATE,
                        ErrorMessageConstants.LEVEL_STATUS.ToLower(),
                        MessageConstants.MESSAGE_FAILED
                    });
            }
        }
        #endregion
    }
}
