using Boek.Core.Constants;
using Boek.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Boek.Core.Data
{
    public partial class BoekCapstoneContext : DbContext
    {
        public BoekCapstoneContext()
        {
        }

        public BoekCapstoneContext(DbContextOptions<BoekCapstoneContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookAuthor> BookAuthors { get; set; }
        public virtual DbSet<BookItem> BookItems { get; set; }
        public virtual DbSet<BookProduct> BookProducts { get; set; }
        public virtual DbSet<BookProductItem> BookProductItems { get; set; }
        public virtual DbSet<Campaign> Campaigns { get; set; }
        public virtual DbSet<CampaignCommission> CampaignCommissions { get; set; }
        public virtual DbSet<CampaignGroup> CampaignGroups { get; set; }
        public virtual DbSet<CampaignLevel> CampaignLevels { get; set; }
        public virtual DbSet<CampaignOrganization> CampaignOrganizations { get; set; }
        public virtual DbSet<CampaignStaff> CampaignStaffs { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerGroup> CustomerGroups { get; set; }
        public virtual DbSet<CustomerOrganization> CustomerOrganizations { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Issuer> Issuers { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<OrganizationMember> OrganizationMembers { get; set; }
        public virtual DbSet<Participant> Participants { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(MessageConstants.BOEK_JSON_FILE, true, true)
                .Build();
                var sqlServer = config[MessageConstants.BOEK_FULL_CONNECTION_STRING];
                optionsBuilder.UseSqlServer(sqlServer);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("Author");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Book");

                entity.Property(e => e.AudioExtraPrice).HasColumnType("money");

                entity.Property(e => e.CoverPrice).HasColumnType("money");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.ImageUrl).IsRequired();

                entity.Property(e => e.IsSeries).HasDefaultValueSql("((0))");

                entity.Property(e => e.Isbn10)
                    .HasMaxLength(50)
                    .HasColumnName("ISBN10");

                entity.Property(e => e.Isbn13)
                    .HasMaxLength(50)
                    .HasColumnName("ISBN13");

                entity.Property(e => e.Language).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.PdfExtraPrice).HasColumnType("money");

                entity.Property(e => e.Size).HasMaxLength(255);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.GenreId)
                    .HasConstraintName("FK__Book__GenreId__2D32A501");

                entity.HasOne(d => d.Issuer)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.IssuerId)
                    .HasConstraintName("FK__Book__IssuerId__2E26C93A");

                entity.HasOne(d => d.Publisher)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.PublisherId)
                    .HasConstraintName("FK__Book__PublisherI__2F1AED73");
            });

            modelBuilder.Entity<BookAuthor>(entity =>
            {
                entity.ToTable("BookAuthor");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.BookAuthors)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK__BookAutho__Autho__300F11AC");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookAuthors)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__BookAutho__BookI__310335E5");
            });

            modelBuilder.Entity<BookItem>(entity =>
            {
                entity.ToTable("BookItem");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookItemBooks)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__BookItem__BookId__31F75A1E");

                entity.HasOne(d => d.ParentBook)
                    .WithMany(p => p.BookItemParentBooks)
                    .HasForeignKey(d => d.ParentBookId)
                    .HasConstraintName("FK__BookItem__Parent__32EB7E57");
            });

            modelBuilder.Entity<BookProduct>(entity =>
            {
                entity.ToTable("BookProduct");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AudioExtraPrice).HasColumnType("money");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Discount).HasDefaultValueSql("((0))");

                entity.Property(e => e.PdfExtraPrice).HasColumnType("money");

                entity.Property(e => e.SalePrice).HasColumnType("money");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.WithAudio).HasDefaultValueSql("((0))");

                entity.Property(e => e.WithPdf).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookProducts)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__BookProdu__BookI__33DFA290");

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.BookProducts)
                    .HasForeignKey(d => d.CampaignId)
                    .HasConstraintName("FK__BookProdu__Campa__34D3C6C9");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.BookProducts)
                    .HasForeignKey(d => d.GenreId)
                    .HasConstraintName("FK__BookProdu__Genre__35C7EB02");

                entity.HasOne(d => d.Issuer)
                    .WithMany(p => p.BookProducts)
                    .HasForeignKey(d => d.IssuerId)
                    .HasConstraintName("FK__BookProdu__Issue__36BC0F3B");
            });

            modelBuilder.Entity<BookProductItem>(entity =>
            {
                entity.ToTable("BookProductItem");

                entity.Property(e => e.AudioExtraPrice).HasColumnType("money");

                entity.Property(e => e.PdfExtraPrice).HasColumnType("money");

                entity.Property(e => e.WithAudio).HasDefaultValueSql("((0))");

                entity.Property(e => e.WithPdf).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookProductItems)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__BookProdu__BookI__37B03374");

                entity.HasOne(d => d.ParentBookProduct)
                    .WithMany(p => p.BookProductItems)
                    .HasForeignKey(d => d.ParentBookProductId)
                    .HasConstraintName("FK__BookProdu__Paren__38A457AD");
            });

            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.ToTable("Campaign");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.ImageUrl).IsRequired();

                entity.Property(e => e.IsRecurring).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CampaignCommission>(entity =>
            {
                entity.ToTable("CampaignCommission");

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.CampaignCommissions)
                    .HasForeignKey(d => d.CampaignId)
                    .HasConstraintName("FK__CampaignC__Campa__39987BE6");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.CampaignCommissions)
                    .HasForeignKey(d => d.GenreId)
                    .HasConstraintName("FK__CampaignC__Genre__3A8CA01F");
            });

            modelBuilder.Entity<CampaignGroup>(entity =>
            {
                entity.ToTable("CampaignGroup");

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.CampaignGroups)
                    .HasForeignKey(d => d.CampaignId)
                    .HasConstraintName("FK__CampaignG__Campa__3B80C458");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.CampaignGroups)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__CampaignG__Group__3C74E891");
            });

            modelBuilder.Entity<CampaignLevel>(entity =>
            {
                entity.ToTable("CampaignLevel");

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.CampaignLevels)
                    .HasForeignKey(d => d.CampaignId)
                    .HasConstraintName("FK__CampaignL__Campa__3D690CCA");

                entity.HasOne(d => d.Level)
                    .WithMany(p => p.CampaignLevels)
                    .HasForeignKey(d => d.LevelId)
                    .HasConstraintName("FK__CampaignL__Level__3E5D3103");
            });

            modelBuilder.Entity<CampaignOrganization>(entity =>
            {
                entity.ToTable("CampaignOrganization");

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.CampaignOrganizations)
                    .HasForeignKey(d => d.CampaignId)
                    .HasConstraintName("FK__CampaignO__Campa__3F51553C");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.CampaignOrganizations)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK__CampaignO__Organ__40457975");
            });

            modelBuilder.Entity<CampaignStaff>(entity =>
            {
                entity.ToTable("CampaignStaff");

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.CampaignStaffs)
                    .HasForeignKey(d => d.CampaignId)
                    .HasConstraintName("FK__CampaignS__Campa__41399DAE");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.CampaignStaffs)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK__CampaignS__Staff__422DC1E7");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Dob).HasColumnType("datetime");

                entity.Property(e => e.Point).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Customer)
                    .HasForeignKey<Customer>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Customer__Id__4321E620");

                entity.HasOne(d => d.Level)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.LevelId)
                    .HasConstraintName("FK__Customer__LevelI__44160A59");
            });

            modelBuilder.Entity<CustomerGroup>(entity =>
            {
                entity.ToTable("CustomerGroup");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerGroups)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__CustomerG__Custo__450A2E92");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.CustomerGroups)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__CustomerG__Group__45FE52CB");
            });

            modelBuilder.Entity<CustomerOrganization>(entity =>
            {
                entity.ToTable("CustomerOrganization");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerOrganizations)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__CustomerO__Custo__46F27704");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.CustomerOrganizations)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK__CustomerO__Organ__47E69B3D");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("Genre");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK__Genre__ParentId__48DABF76");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Issuer>(entity =>
            {
                entity.ToTable("Issuer");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Issuer)
                    .HasForeignKey<Issuer>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Issuer__Id__49CEE3AF");
            });

            modelBuilder.Entity<Level>(entity =>
            {
                entity.ToTable("Level");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AvailableDate).HasColumnType("datetime");

                entity.Property(e => e.CancelledDate).HasColumnType("datetime");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerEmail).HasMaxLength(255);

                entity.Property(e => e.CustomerName).HasMaxLength(255);

                entity.Property(e => e.CustomerPhone).HasMaxLength(50);

                entity.Property(e => e.Freight).HasColumnType("money");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.ReceivedDate).HasColumnType("datetime");

                entity.Property(e => e.ShippedDate).HasColumnType("datetime");

                entity.Property(e => e.ShippingDate).HasColumnType("datetime");

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CampaignId)
                    .HasConstraintName("FK__Order__CampaignI__4AC307E8");

                entity.HasOne(d => d.CampaignStaff)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CampaignStaffId)
                    .HasConstraintName("FK__Order__CampaignS__4BB72C21");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Order__CustomerI__4CAB505A");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("OrderDetail");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.WithAudio).HasDefaultValueSql("((0))");

                entity.Property(e => e.WithPdf).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.BookProduct)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.BookProductId)
                    .HasConstraintName("FK__OrderDeta__BookP__4D9F7493");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrderDeta__Order__4E9398CC");
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.ToTable("Organization");

                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<OrganizationMember>(entity =>
            {
                entity.ToTable("OrganizationMember");

                entity.Property(e => e.EmailDomain)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.OrganizationMembers)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK__Organizat__Organ__4F87BD05");
            });

            modelBuilder.Entity<Participant>(entity =>
            {
                entity.ToTable("Participant");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Note).IsRequired();

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.Participants)
                    .HasForeignKey(d => d.CampaignId)
                    .HasConstraintName("FK__Participa__Campa__507BE13E");

                entity.HasOne(d => d.Issuer)
                    .WithMany(p => p.Participants)
                    .HasForeignKey(d => d.IssuerId)
                    .HasConstraintName("FK__Participa__Issue__51700577");
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.ToTable("Publisher");

                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ImageUrl).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("Schedule");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.CampaignOrganization)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.CampaignOrganizationId)
                    .HasConstraintName("FK__Schedule__Campai__526429B0");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
