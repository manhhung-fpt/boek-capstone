using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.BookProducts.Mobile;
using Boek.Infrastructure.Requests.BookProducts.Mobile;
using Boek.Service.Commons;
using Boek.Infrastructure.ViewModels.Campaigns.Mobile;

namespace Boek.Service.Interfaces.Mobile
{
    public interface IBookProductMobileService
    {
        #region Gets
        /// <summary>
        /// Get a hierarchical book products (<paramref name="filter"/>)
        /// </summary>
        /// <param name="filter"></param>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched campaign.
        /// </exception>
        /// <returns></returns>
        HierarchicalBookProductsViewModel GetHierarchicalBookProducts(HierarchicalBookProductsRequestModel filter);
        /// <summary>
        /// Get an unhierarchical book products (<paramref name="filter"/>)
        /// </summary>
        /// <param name="filter"></param>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched campaign.
        /// </exception>
        /// <returns></returns>
        UnhierarchicalBookProductsViewModel GetUnhierarchicalBookProducts(UnhierarchicalBookProductsRequestModel filter);
        /// <summary>
        /// Get mobile book products
        /// (<paramref name="filter"/>,<paramref name="paging"/>
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns>
        /// </returns>
        BaseResponsePagingModel<MobileBookProductViewModel> GetMobileBookProducts(BookProductMobileRequestModel filter, PagingModel paging);
        /// <summary>
        /// Get mobile book product by id (<paramref name="id"/>)
        /// </summary>
        /// <param name="Id"></param>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched book product.
        /// </exception>
        /// <returns></returns>
        MobileBookProductViewModel GetMobileBookProductById(Guid? id);
        #endregion

        #region Generates
        /// <summary>
        /// Generate list of hierarchical book products by campaign id
        /// </summary>
        /// <param name="CampaignId"></param>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched campaign.
        /// </exception>
        /// <returns></returns>
        List<HierarchicalBookProductsViewModel> GenerateHierarchicalBookProducts(int? CampaignId);
        /// <summary>
        /// Generate list of unhierarchical book products by campaign id
        /// </summary>
        /// <param name="CampaignId"></param>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched campaign.
        /// </exception>
        /// <returns></returns>
        List<UnhierarchicalBookProductsViewModel> GenerateUnhierarchicalBookProducts(int? CampaignId);
        /// <summary>
        /// Generate list of hierarchical book products by campaign
        /// </summary>
        /// <param name="campaign"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        List<HierarchicalBookProductsViewModel> GenerateHierarchicalBookProducts(CampaignMobileViewModel campaign);
        /// <summary>
        /// Generate list of unhierarchical book products by campaign
        /// </summary>
        /// <param name="campaign"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        List<UnhierarchicalBookProductsViewModel> GenerateUnhierarchicalBookProducts(CampaignMobileViewModel campaigns);
        #endregion
    }
}