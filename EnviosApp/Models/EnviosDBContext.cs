using Microsoft.EntityFrameworkCore;

namespace EnviosApp.Models {
    public class EnviosDBContext : DbContext {
        public EnviosDBContext(DbContextOptions<EnviosDBContext> dbContextOptions) : base(dbContextOptions) {        }

        public static void Initialize(EnviosDBContext context) {
            if (!context.User.Any()) {
                var users = new User[] {
                    new User { Name = "Facundo Delgado", Password = "123", Role= "admin", UserName= "Facundo" },
                    new User { Name = "Roberto Perez", Password = "robert", Role= "user", UserName= "robert" }
                };
                context.User.AddRange(users);
                context.SaveChanges();
            }
            if (!context.Country.Any()) {
                var countries = new Country[] {

                        new Country { Alpha = "AF", Name = "Afghanistan" },
                        new Country { Alpha = "AL", Name = "Albania" },
                        new Country { Alpha = "DZ", Name = "Algeria" },
                        new Country { Alpha = "AS", Name = "American Samoa" },
                        new Country { Alpha = "AD", Name = "Andorra" },
                        new Country { Alpha = "AO", Name = "Angola" },
                        new Country { Alpha = "AG", Name = "Antigua and Barbuda" },
                        new Country { Alpha = "AR", Name = "Argentina" },
                        new Country { Alpha = "AM", Name = "Armenia" },
                        new Country { Alpha = "AU", Name = "Australia" },
                        new Country { Alpha = "AT", Name = "Austria" },
                        new Country { Alpha = "AZ", Name = "Azerbaijan" },
                        new Country { Alpha = "BS", Name = "Bahamas" },
                        new Country { Alpha = "BH", Name = "Bahrain" },
                        new Country { Alpha = "BD", Name = "Bangladesh" },
                        new Country { Alpha = "BB", Name = "Barbados" },
                        new Country { Alpha = "BY", Name = "Belarus" },
                        new Country { Alpha = "BE", Name = "Belgium" },
                        new Country { Alpha = "BZ", Name = "Belize" },
                        new Country { Alpha = "BJ", Name = "Benin" },
                        new Country { Alpha = "BT", Name = "Bhutan" },
                        new Country { Alpha = "BO", Name = "Bolivia" },
                        new Country { Alpha = "BA", Name = "Bosnia and Herzegovina" },
                        new Country { Alpha = "BW", Name = "Botswana" },
                        new Country { Alpha = "BR", Name = "Brazil" },
                        new Country { Alpha = "BN", Name = "Brunei" },
                        new Country { Alpha = "BG", Name = "Bulgaria" },
                        new Country { Alpha = "BF", Name = "Burkina Faso" },
                        new Country { Alpha = "BI", Name = "Burundi" },
                        new Country { Alpha = "CV", Name = "Cabo Verde" },
                        new Country { Alpha = "KH", Name = "Cambodia" },
                        new Country { Alpha = "CM", Name = "Cameroon" },
                        new Country { Alpha = "CA", Name = "Canada" },
                        new Country { Alpha = "CF", Name = "Central African Republic" },
                        new Country { Alpha = "TD", Name = "Chad" },
                        new Country { Alpha = "CL", Name = "Chile" },
                        new Country { Alpha = "CN", Name = "China" },
                        new Country { Alpha = "CO", Name = "Colombia" },
                        new Country { Alpha = "KM", Name = "Comoros" },
                        new Country { Alpha = "CG", Name = "Congo (Congo-Brazzaville)" },
                        new Country { Alpha = "CD", Name = "Congo (Democratic Republic of the)" },
                        new Country { Alpha = "CR", Name = "Costa Rica" },
                        new Country { Alpha = "HR", Name = "Croatia" },
                        new Country { Alpha = "CU", Name = "Cuba" },
                        new Country { Alpha = "CY", Name = "Cyprus" },
                        new Country { Alpha = "CZ", Name = "Czechia (Czech Republic)" },
                        new Country { Alpha = "DK", Name = "Denmark" },
                        new Country { Alpha = "DJ", Name = "Djibouti" },
                        new Country { Alpha = "DM", Name = "Dominica" },
                        new Country { Alpha = "DO", Name = "Dominican Republic" },
                        new Country { Alpha = "EC", Name = "Ecuador" },
                        new Country { Alpha = "EG", Name = "Egypt" },
                        new Country { Alpha = "SV", Name = "El Salvador" },
                        new Country { Alpha = "GQ", Name = "Equatorial Guinea" },
                        new Country { Alpha = "ER", Name = "Eritrea" },
                        new Country { Alpha = "EE", Name = "Estonia" },
                        new Country { Alpha = "SZ", Name = "Eswatini" },
                        new Country { Alpha = "ET", Name = "Ethiopia" },
                        new Country { Alpha = "FJ", Name = "Fiji" },
                        new Country { Alpha = "FI", Name = "Finland" },
                        new Country { Alpha = "FR", Name = "France" },
                        new Country { Alpha = "GA", Name = "Gabon" },
                        new Country { Alpha = "GM", Name = "Gambia" },
                        new Country { Alpha = "GE", Name = "Georgia" },
                        new Country { Alpha = "DE", Name = "Germany" },
                        new Country { Alpha = "GH", Name = "Ghana" },
                        new Country { Alpha = "GR", Name = "Greece" },
                        new Country { Alpha = "GD", Name = "Grenada" },
                        new Country { Alpha = "GT", Name = "Guatemala" },
                        new Country { Alpha = "GN", Name = "Guinea" },
                        new Country { Alpha = "GW", Name = "Guinea-Bissau" },
                        new Country { Alpha = "GY", Name = "Guyana" },
                        new Country { Alpha = "HT", Name = "Haiti" },
                        new Country { Alpha = "HN", Name = "Honduras" },
                        new Country { Alpha = "HU", Name = "Hungary" },
                        new Country { Alpha = "IS", Name = "Iceland" },
                        new Country { Alpha = "IN", Name = "India" },
                        new Country { Alpha = "ID", Name = "Indonesia" },
                        new Country { Alpha = "IR", Name = "Iran" },
                        new Country { Alpha = "IQ", Name = "Iraq" },
                        new Country { Alpha = "IE", Name = "Ireland" },
                        new Country { Alpha = "IL", Name = "Israel" },
                        new Country { Alpha = "IT", Name = "Italy" },
                        new Country { Alpha = "JM", Name = "Jamaica" },
                        new Country { Alpha = "JP", Name = "Japan" },
                        new Country { Alpha = "JO", Name = "Jordan" },
                        new Country { Alpha = "KZ", Name = "Kazakhstan" },
                        new Country { Alpha = "KE", Name = "Kenya" },
                        new Country { Alpha = "KI", Name = "Kiribati" },
                        new Country { Alpha = "KP", Name = "Korea (North)" },
                        new Country { Alpha = "KR", Name = "Korea (South)" },
                        new Country { Alpha = "KW", Name = "Kuwait" },
                        new Country { Alpha = "KG", Name = "Kyrgyzstan" },
                        new Country { Alpha = "LA", Name = "Laos" },
                        new Country { Alpha = "LV", Name = "Latvia" },
                        new Country { Alpha = "LB", Name = "Lebanon" },
                        new Country { Alpha = "LS", Name = "Lesotho" },
                        new Country { Alpha = "LR", Name = "Liberia" },
                        new Country { Alpha = "LY", Name = "Libya" },
                        new Country { Alpha = "LI", Name = "Liechtenstein" },
                        new Country { Alpha = "LT", Name = "Lithuania" },
                        new Country { Alpha = "LU", Name = "Luxembourg" },
                        new Country { Alpha = "MG", Name = "Madagascar" },
                        new Country { Alpha = "MW", Name = "Malawi" },
                        new Country { Alpha = "MY", Name = "Malaysia" },
                        new Country { Alpha = "MV", Name = "Maldives" },
                        new Country { Alpha = "ML", Name = "Mali" },
                        new Country { Alpha = "MT", Name = "Malta" },
                        new Country { Alpha = "MH", Name = "Marshall Islands" },
                        new Country { Alpha = "MR", Name = "Mauritania" },
                        new Country { Alpha = "MU", Name = "Mauritius" },
                        new Country { Alpha = "MX", Name = "Mexico" },
                        new Country { Alpha = "FM", Name = "Micronesia" },
                        new Country { Alpha = "MD", Name = "Moldova" },
                        new Country { Alpha = "MC", Name = "Monaco" },
                        new Country { Alpha = "MN", Name = "Mongolia" },
                        new Country { Alpha = "ME", Name = "Montenegro" },
                        new Country { Alpha = "MA", Name = "Morocco" },
                        new Country { Alpha = "MZ", Name = "Mozambique" },
                        new Country { Alpha = "MM", Name = "Myanmar (Burma)" },
                        new Country { Alpha = "NA", Name = "Namibia" },
                        new Country { Alpha = "NR", Name = "Nauru" },
                        new Country { Alpha = "NP", Name = "Nepal" },
                        new Country { Alpha = "NL", Name = "Netherlands" },
                        new Country { Alpha = "NZ", Name = "New Zealand" },
                        new Country { Alpha = "NI", Name = "Nicaragua" },
                        new Country { Alpha = "NE", Name = "Niger" },
                        new Country { Alpha = "NG", Name = "Nigeria" },
                        new Country { Alpha = "NO", Name = "Norway" },
                        new Country { Alpha = "OM", Name = "Oman" },
                        new Country { Alpha = "PK", Name = "Pakistan" },
                        new Country { Alpha = "PW", Name = "Palau" },
                        new Country { Alpha = "PA", Name = "Panama" },
                        new Country { Alpha = "PG", Name = "Papua New Guinea" },
                        new Country { Alpha = "PY", Name = "Paraguay" },
                        new Country { Alpha = "PE", Name = "Peru" },
                        new Country { Alpha = "PH", Name = "Philippines" },
                        new Country { Alpha = "PL", Name = "Poland" },
                        new Country { Alpha = "PT", Name = "Portugal" },
                        new Country { Alpha = "QA", Name = "Qatar" },
                        new Country { Alpha = "RO", Name = "Romania" },
                        new Country { Alpha = "RU", Name = "Russia" },
                        new Country { Alpha = "RW", Name = "Rwanda" },
                        new Country { Alpha = "WS", Name = "Samoa" },
                        new Country { Alpha = "SM", Name = "San Marino" },
                        new Country { Alpha = "ST", Name = "Sao Tome and Principe" },
                        new Country { Alpha = "SA", Name = "Saudi Arabia" },
                        new Country { Alpha = "SN", Name = "Senegal" },
                        new Country { Alpha = "RS", Name = "Serbia" },
                        new Country { Alpha = "SC", Name = "Seychelles" },
                        new Country { Alpha = "SL", Name = "Sierra Leone" },
                        new Country { Alpha = "SG", Name = "Singapore" },
                        new Country { Alpha = "SK", Name = "Slovakia" },
                        new Country { Alpha = "SI", Name = "Slovenia" },
                        new Country { Alpha = "SB", Name = "Solomon Islands" },
                        new Country { Alpha = "SO", Name = "Somalia" },
                        new Country { Alpha = "ZA", Name = "South Africa" },
                        new Country { Alpha = "ES", Name = "Spain" },
                        new Country { Alpha = "LK", Name = "Sri Lanka" },
                        new Country { Alpha = "SD", Name = "Sudan" },
                        new Country { Alpha = "SR", Name = "Suriname" },
                        new Country { Alpha = "SE", Name = "Sweden" },
                        new Country { Alpha = "CH", Name = "Switzerland" },
                        new Country { Alpha = "SY", Name = "Syria" },
                        new Country { Alpha = "TW", Name = "Taiwan" },
                        new Country { Alpha = "TJ", Name = "Tajikistan" },
                        new Country { Alpha = "TZ", Name = "Tanzania" },
                        new Country { Alpha = "TH", Name = "Thailand" },
                        new Country { Alpha = "TL", Name = "Timor-Leste" },
                        new Country { Alpha = "TG", Name = "Togo" },
                        new Country { Alpha = "TO", Name = "Tonga" },
                        new Country { Alpha = "TT", Name = "Trinidad and Tobago" },
                        new Country { Alpha = "TN", Name = "Tunisia" },
                        new Country { Alpha = "TR", Name = "Turkey" },
                        new Country { Alpha = "TM", Name = "Turkmenistan" },
                        new Country { Alpha = "TV", Name = "Tuvalu" },
                        new Country { Alpha = "UG", Name = "Uganda" },
                        new Country { Alpha = "UA", Name = "Ukraine" },
                        new Country { Alpha = "AE", Name = "United Arab Emirates" },
                        new Country { Alpha = "GB", Name = "United Kingdom" },
                        new Country { Alpha = "US", Name = "United States of America" },
                        new Country { Alpha = "UY", Name = "Uruguay" },
                        new Country { Alpha = "UZ", Name = "Uzbekistan" },
                        new Country { Alpha = "VU", Name = "Vanuatu" },
                        new Country { Alpha = "VE", Name = "Venezuela" },
                        new Country { Alpha = "VN", Name = "Vietnam" },
                        new Country { Alpha = "YE", Name = "Yemen" },
                        new Country { Alpha = "ZM", Name = "Zambia" },
                        new Country { Alpha = "ZW", Name = "Zimbabwe" }
                    };
                context.Country.AddRange(countries);

                //guardamos
                context.SaveChanges();
            }
            if (!context.Provider.Any()) {
                var providers = new Provider[] {
                    new Provider { Name = "DHL" }

                };
                context.Provider.AddRange(providers);
                context.SaveChanges();
            }
            if (!context.ServiceType.Any()) {
                var servicetypes = new ServiceType[] {
                    new ServiceType { Name = "DHL Economy", ProviderId = 1, PriceMultiplier = 0.5m }

                };
                context.ServiceType.AddRange(servicetypes);
                context.SaveChanges();
            }
            if (!context.Zone.Any()) {
                var zones= new Zone[] {
                    new Zone { Name = "Zona 1", ProviderId = 1, BasePrice = 2.0m }

                };
                context.Zone.AddRange(zones);
                context.SaveChanges();
            }
            if (!context.ZoneCountry.Any()) {
                var zones = new ZoneCountry[] {
                    new ZoneCountry { CountryId = 1, ZoneId = 1 }

                };
                context.ZoneCountry.AddRange(zones);
                context.SaveChanges();
            }


        }

        public DbSet<User> User {get; set;}
        public DbSet<Client> Client { get; set; }
        public DbSet<Country> Country{ get; set; }
        public DbSet<Provider> Provider{ get; set; }
        public DbSet<Receiver> Receiver{ get; set; }
        public DbSet<ServiceType> ServiceType { get; set; }
        public DbSet<Waybill> Waybill { get; set; }
        public DbSet<Zone> Zone { get; set; }
        public DbSet<ZoneCountry> ZoneCountry{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            // Configuración para ServiceType
            modelBuilder.Entity<ServiceType>()
                .Property(st => st.PriceMultiplier)
                .HasPrecision(18, 4); // 18 dígitos totales, 4 decimales

            // Configuración para Zone
            modelBuilder.Entity<Zone>()
                .Property(z => z.BasePrice)
                .HasPrecision(18, 2); // 18 dígitos totales, 2 decimales

            // Configuración para Waybill
            modelBuilder.Entity<Waybill>()
                .Property(w => w.Depth)
                .HasPrecision(18, 3); // 18 dígitos totales, 3 decimales
            modelBuilder.Entity<Waybill>()
                .Property(w => w.Height)
                .HasPrecision(18, 3);
            modelBuilder.Entity<Waybill>()
                .Property(w => w.Price)
                .HasPrecision(18, 2);
            modelBuilder.Entity<Waybill>()
                .Property(w => w.Weight)
                .HasPrecision(18, 3);
            modelBuilder.Entity<Waybill>()
                .Property(w => w.Width)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Waybill>()
            .HasOne(w => w.ServiceType)
            .WithMany()
            .HasForeignKey(w => w.ServiceTypeId)
            .OnDelete(DeleteBehavior.NoAction);  // Cambiar a NoAction

            modelBuilder.Entity<Waybill>()
                .HasOne(w => w.Provider)
                .WithMany()
                .HasForeignKey(w => w.ProviderId)
                .OnDelete(DeleteBehavior.NoAction);  // Cambiar a NoAction
        }
    }
}
