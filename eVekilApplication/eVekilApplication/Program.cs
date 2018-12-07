using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using eVekilApplication.Data;
using eVekilApplication.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace eVekilApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost webHost = CreateWebHostBuilder(args).Build(); 

            using (IServiceScope scopedService = webHost.Services.CreateScope())
            {
                using (EvekilDb dbContext = scopedService.ServiceProvider.GetRequiredService<EvekilDb>())
                {
                    if (!dbContext.Advocates.Any())
                    {
                        #region Advocates
                        Advocate NihadAliyev = new Advocate()
                        {
                            Name = "Nihad",
                            Surname = "Əliyev",
                            Email = "nihad@hgn.az",
                            Phone = 0502503575
                        };
                        #endregion
                        dbContext.Advocates.Add(NihadAliyev);
                        dbContext.SaveChanges();
                    }
                    if (!dbContext.Subcategories.Any() && !dbContext.Categories.Any())
                    {
                        #region Categories
                        Category IR = new Category()
                        {
                            Name = "İnsan Resursları",
                            Description = @"Bu bölmədə kadrlar şöbəsinin faəliyyətinə aid müxtəlif sənəd nümunələri, o cümlədən əmr formaları, əmək müqavilələri, əmək müqavilələrinə əlavələr, vəzifə təlimatları, aktlar, izahat formaları, ərizələr, əmr kitabları və s. yerləşdirilmişdir.
                                           Diqqətinizə təqdim edilən bu sənəd nümunələri Azərbaycan Respublikasında fəaliyyət göstərən müxtəlif təşkilatlar tərəfindən istifadə edilməkdədir."


                        };
                        Category MS = new Category()
                        {
                            Name = "Məhkəmə Sənədləri",
                            Description = @"Əsasən mülki və iqtisadi mübahisələr üzrə məhkəməyə qədər və məhkəmə araşdırması dövründə tərtib edilən sənəd nümunələri bu bölmədə sizin diqqətinizə təqdim edilir.
                            Sənəd nümunələri arasında təmənnasız təqdim edilən bəzi iddia ərizələri formaları ilə yanaşı, müxtəlif məzmunlu və formalı vəsatətlər, apellyasiya şikayətləri, kassasiya şikayətləri formaları və s. mövcuddur. Sənəd nümunələri Azərbaycan Respublikası Vəkillər Kollegiyasının üzvləri tərəfindən tərtib edilmişdir. Sənəd nümunələrindən real məhkəmə işlərində istifadə edilmişdir."


                        };
                        Category M = new Category()
                        {
                            Name = "Müqavilələr",
                            Description = @"Azərbaycan Respublikasının qanunvericiliyinə uyğun tərtib edilən müxtəlif müqavilə növləri. Təqdim edilən bütün müqavilə növləri təcrübədə istifadə edilmişdir.
                                           Müqavilələr arasında tez-tez istifadə edilən alğı-satqı, bağışlama, podrat, xidmət müqavilələri ilə yanaşı Azərbaycan işgüzar adətlərində yeni-yeni rast gəlinən autsorsinq, birgə əməliyyat sazişləri nümunələri də daxil edilmişdir."
                        };
                        Category SS = new Category()
                        {
                            Name = "Sair Sənədlər",
                            Description = @"Yuxarıdakı təsnifata yer almamış sənəd nümunələrini hazırki bölmədə yerləşdirərək diqqətinizə çatdırırıq. Bu bölmədə hüquqi şəxsin təsis sənədləri nümunələri, informasiya sorğuları, şikayət ərizələri, prtokol formaları, etibarnamələr, müraciət ərizələri və s. sənəd nümunələri yerləşdirilmişdir.
                                           We understand business. That's why we begin every project with a workshop — crafting a one-of-a-kind, unique strategy that is designed to help you win."
                        };
                        #endregion

                        dbContext.Categories.AddRange(IR, M, MS, SS);
                        dbContext.SaveChanges();

                        #region Subcategories

                        #region InsanResurslariSubcategoriyasi
                        Subcategory EM = new Subcategory()
                        {
                            Name = "Əmək Müqaviləsi",
                            Category = IR
                        };
                        Subcategory EF = new Subcategory()
                        {
                            Name = "Əmr Formaları",
                            Category = IR
                        };
                        Subcategory VT = new Subcategory()
                        {
                            Name = "Vəzifə Təlimatları",
                            Category = IR
                        };
                        Subcategory E = new Subcategory()
                        {
                            Name = "Ərizələr",
                            Category = IR
                        };
                        Subcategory EK = new Subcategory()
                        {
                            Name = "Əmr Kitabları",
                            Category = IR
                        };
                        Subcategory EV = new Subcategory()
                        {
                            Name = "Ezamiyyə Vərəqələri",
                            Category = IR
                        };
                        Subcategory A = new Subcategory()
                        {
                            Name = "Aktlar",
                            Category = IR
                        };

                        #endregion
                        #region MehkemeSenedleriSubcategoriyasi
                        Subcategory IE = new Subcategory()
                        {
                            Name = "İddia Ərizələri",
                            Category = MS
                        };
                        Subcategory AS = new Subcategory()
                        {
                            Name = "Apelyasiya Şikayətləri",
                            Category = MS
                        };
                        Subcategory KS = new Subcategory()
                        {
                            Name = "Kassasiya Şikayətləri",
                            Category = MS
                        };
                        Subcategory V = new Subcategory()
                        {
                            Name = "Vəsatətlər",
                            Category = MS
                        };
                        Subcategory BS = new Subcategory()
                        {
                            Name = "Barışıq Sazişləri",
                            Category = MS
                        };
                        Subcategory QIE = new Subcategory()
                        {
                            Name = "Qarşılıqlı İddia Ərizələri",
                            Category = MS
                        };
                        Subcategory ET = new Subcategory()
                        {
                            Name = "Etirazlar",
                            Category = MS
                        };
                        #endregion
                        #region MuqavilelerSubcategoriyasi
                        Subcategory ASM = new Subcategory()
                        {
                            Name = "Alğı-satqı Müqavilələri",
                            Category = M
                        };
                        Subcategory PM = new Subcategory()
                        {
                            Name = "Podrat Müqavilələri",
                            Category = M
                        };
                        Subcategory XM = new Subcategory()
                        {
                            Name = "Xidmət Müqavilələri",
                            Category = M
                        };
                        Subcategory BM = new Subcategory()
                        {
                            Name = "Borc Müqavilələri",
                            Category = M
                        };
                        Subcategory DM = new Subcategory()
                        {
                            Name = "Daşınma Müqavilələri",
                            Category = M
                        };
                        Subcategory ME = new Subcategory()
                        {
                            Name = "Müqavilələrə Əlavələr",
                            Category = M
                        };
                        Subcategory IM = new Subcategory()
                        {
                            Name = "İcarə Müqavilələri" +
                            "",
                            Category = M
                        };
                        #endregion
                        #region SairSenedlerSubcategoriyasi
                        Subcategory HSUY = new Subcategory()
                        {
                            Name = "Hüquqi Şəxsin Ümumi Yığıncağının Qərarı",
                            Category = SS
                        };
                        Subcategory EUMEF = new Subcategory()
                        {
                            Name = "Əfv üçün Müraciət Ərizə Forması",
                            Category = SS
                        };
                        Subcategory ES = new Subcategory()
                        {
                            Name = "Etibarnamələr",
                            Category = SS
                        };
                        Subcategory TQ = new Subcategory()
                        {
                            Name = "Təsisçinin Qərarı",
                            Category = SS
                        };
                        Subcategory VF = new Subcategory()
                        {
                            Name = "Vəsiyyətnamə Formaları",
                            Category = SS
                        };
                        Subcategory Akt = new Subcategory()
                        {
                            Name = "Aktlar",
                            Category = SS
                        };
                        Subcategory QF = new Subcategory()
                        {
                            Name = "Qərar Formaları",
                            Category = SS
                        };
                        #endregion
                        #endregion
                        dbContext.Subcategories.AddRange(EM, EF, VT, E, EK, EV, A, IE, AS, KS, V, BS, QIE, ET, ASM, PM, XM, BM, DM, ME, IM, HSUY, EUMEF, ES, TQ, VF, Akt, QF);
                        dbContext.SaveChanges();
                    }
                    UserAndRoleCreater.CreateAsync(scopedService, dbContext).Wait();

                }
            }

            webHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
