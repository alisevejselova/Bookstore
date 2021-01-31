using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.Data;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Bookstore.Models
{
    public class SeedData
    {
        public static async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            IdentityResult roleResult;
            //Adding Admin Role
            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck)
            {
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));
            }
            roleCheck = await RoleManager.RoleExistsAsync("User");
            if (!roleCheck)
            {
                roleResult = await RoleManager.CreateAsync(new IdentityRole("User"));
            }


            AppUser user = await UserManager.FindByEmailAsync("andromeda@gmail.com");
            if (user == null)
            {
                var User = new AppUser();
                User.Email = "andromeda@gmail.com";
                User.UserName = "Andromeda";
                User.Role = "Admin";
                string userPWD = "Admin123@";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                //Add default User to Role Admin      
                if (chkUser.Succeeded)
                {
                    var result1 = await UserManager.AddToRoleAsync(User, "Admin");
                }
            }
        }

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookstoreContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<BookstoreContext>>()))
            {
                CreateUserRoles(serviceProvider).Wait();
                // Look for any movies.
               if (context.Movie.Any() || context.Book.Any() || context.Author.Any() || context.User.Any())
                {
                    return;   // DB has been seeded
               }
            

                context.Author.AddRange(
                    new Author
                    {
                        Picture = "donatokarizi.jpg",
                        FirstName = "Донато",
                        LastName = "Каризи",
                        DateofBirth = DateTime.Parse("1973-3-15"),
                        Biography = "47-годишниот автор е роден во 1973 година и студирал право и криминологија. Каризи од 1999 година работи како ТВ сценарист. Првиот роман на Каризи, „Шепнувачот“ му донел 5 меѓународни книжевни награди, бил продаден во над 20 држави и преведен на француски, дански, хебрејски и виетнамски. Творечкиот опус на Каризи константно се зголемува, а неговата последна книга L’uomo del labirinto (Човекот во лавиринтот) издадена во 2017 година повторно доживеа неверојатен успех со висока 4.5 оцена на скалата до 5. Морничавоста и напнатоста на дејствието, како и генијалните заплети, пресврти и добро исткаените ликови се она што го прават Каризи еден од најдобрите автори на жанрот трилер на денешницата.",
                        Rewards = "David di Donatello",
                        Books = "Шепнувачот"
                    },
                     new Author
                     {
                         Picture = "trumankapoti.jpg",
                         FirstName = "Труман",
                         LastName = "Капоти",
                         DateofBirth = DateTime.Parse("1924-9-30"),
                         DateofDeath = DateTime.Parse("1984-8-25"),
                         Biography = "Труман Капоти е американски романописец, сценарист, драматург и глумец, многу од чии раскази, романи, драми и документаристики се признаваат како литературни класики, вклучувајќи ја новелата Појадок кај Тифани и Во Ладна Крв - пресвртница во популарната култура.",
                         Rewards = "Edgar award и Emmy award",
                         Books = "Појадок кај Тифани"
                     },
                        new Author
                        {
                            Picture = "agatakristi.jpg",
                            FirstName = "Агата",
                            LastName = "Кристи",
                            DateofBirth = DateTime.Parse("1890-9-15"),
                            DateofDeath = DateTime.Parse("1976-1-12"),
                            Biography = "Агата Кристи позната под псевдинимот Мери Вестакот и е Британката која го освои светот со мистериозните, најчесто криминалистички романи кои ве заробуваат во својот заплет, а водушевуваат со крајот. Според Гинисовата книга на рекорди, Агата е најпродаваниот автор на сите времиња. Нејзините книги се продадени во повеќе од 2 милијарди примероци ширум светот и се преведени на повеќе од 50 јазици. Само Библијата и шекспировите дела доживеале поголема продажба од нејзините романи.",
                            Rewards = "Edgar award и Anthony award",
                            Books = "Убиство во Ориент-Експрес"
                        },
                        new Author
                        {
                            Picture = "dzejnostin.jpg",
                            FirstName = "Џејн",
                            LastName = "Остин",
                            DateofBirth = DateTime.Parse("1775-12-16"),
                            DateofDeath = DateTime.Parse("1817-7-18"),
                            Biography = "Џејн Остин била англиска романсиерка, чиј реализам, остри коментари за општеството и одличната употреба на слободниот индиректен говор, пародијата и иронијата и го овозможиле местото на најчитаната и најсаканата писателка во англиската книжевност.",
                            Rewards = "USC Scripter Award",
                            Books = "Гордост и предрасуда"
                        },
                       new Author
                       {
                           Picture = "petre-m.andreevski.jpg",
                           FirstName = "Петре",
                           LastName = "М. Андреевски",
                           DateofBirth = DateTime.Parse("1934-6-25"),
                           DateofDeath = DateTime.Parse("2006-9-25"),
                           Biography = "Петре Миланов Андреевски поет, романсиер, раскажувач, драмски автор. Долги години работел како уредник во Македонската радиотелевизија. Член е на МАНУ од мај 2000 г. Член е на ДПМ (1964) и на Македонскиот ПЕН-центар. Пишувал стихови, раскази, романи, драми и филмски сценарија. Андреевски создаде дело што не само што ја смени свеста за литературата кај македонскиот читател, кое не само што иницира цели генерации што се угледуваа на тој модел, туку ја смени и македонската критичка свест.",
                           Rewards = "11 Октомври, Браќа Миладиновци, Рациново признание и Стале Попов",
                           Books = "Дениција"
                       },
                       new Author
                       {
                           Picture = "antoine.jpg",
                           FirstName = "Антоан Де",
                           LastName = "Сент-Егзипери",
                           DateofBirth = DateTime.Parse("1900-6-29"),
                           DateofDeath = DateTime.Parse("1944-7-31"),
                           Biography = "Антоан де Сент-Егзипери, француски авијатичар и писател чии дела се уникатно сведоштво на пилотот и воинот кој гледаше на авантурите и опасностите низ очите на еден поет. Неговото дело Малиот принц стана модерна класика.",
                           Rewards = "Croix de Guerre, Prix Femina,Retro Hugo и Grand Prix",
                           Books = "Малиот Принц"
                       }
                );
                context.SaveChanges();

                context.Book.AddRange(
                    new Book
                    {
                        Picture = "shepnuvachot.jpg",
                        Authorid = context.Author.Single(d => d.FirstName == "Донато" && d.LastName == "Каризи").AuthorID,
                        Title = "Шепнувачот",
                        OriginalTitle = "The whisperer",
                        Genre = "Трилер",
                        Synopsis = "Исчезнуваат пет девојчиња на возраст од осум до тринаесет години. Во меѓувреме пронајдено е местото на кое можеби се закопани, а го пронаоѓаат две момчиња со нивното куче сосем случајно. Но тоа не се нивните тела, сè што пронаоѓаат се неколку леви раце закопани во круг, поточно шест леви раце, а пет девојчиња се пријавени како исчезнати. Некаде уште едно девојче е киднапирано но сè уште не е пријавено како исчезнато.",
                        NumberofPages = 597,
                        Price = 400,
                        ReleaseDate = DateTime.Parse("2009-1-15"),
                        Publisher = "Матица"
                    },
                     new Book
                     {
                         Picture = "pojadokkajtifani.jpg",
                         Authorid = context.Author.Single(d => d.FirstName == "Труман" && d.LastName == "Капоти").AuthorID,
                         Title = "Појадок кај Тифани",
                         OriginalTitle = "Breakfast at Tiffany’s",
                         Genre = "Новела",
                         Synopsis = "Њујорк и продавницата за скапоцен накит „Кај Тифани“ треба да спојат еден вечен сонувач и една лесна девојка која ги врти мажите на малиот прст. Во култниот истоимен филм Холи Голајтли ја игра Одри Хепберн, која просто брилјира во улогата на џебна Венера. Неповторлива книга која и ден денес влијае на повеќе форми на музика, мода, современа култура.",
                         NumberofPages = 128,
                         Price = 200,
                         ReleaseDate = DateTime.Parse("1958-10-16"),
                         Publisher = "Антолог"
                     },
                     new Book
                     {
                         Picture = "orientekspres.jpeg",
                         Authorid = context.Author.Single(d => d.FirstName == "Агата" && d.LastName == "Кристи").AuthorID,
                         Title = "Убиство во Ориент-Експрес",
                         OriginalTitle = "Murder on the Orient Express",
                         Genre = "Мистерија",
                         Synopsis = "Во еден од најславните криминалистички романи излезени од перото на Агата Кристи, нејзиниот омилен мал белгиски детектив, Херкул Поаро, патува со легендарниот Ориент-експрес, надевајќи се на идиличен одмор. Во џет-сет друштвото, со добро јадење и пиење изгледаше дека пред Поаро е пријатно патување. Но, по немирната ноќ, омилениот јунак на Агата Кристи одеднаш се наоѓа во центарот на трагедијата: eден човек е убиен, а секој  од сопатниците би можел да биде виновник за злосторството.",
                         NumberofPages = 208,
                         Price = 250,
                         ReleaseDate = DateTime.Parse("1934-1-1"),
                         Publisher = "МПМ-Скопје"
                     },
                       new Book
                       {
                           Picture = "gordostipredrasuda.jpg",
                           Authorid = context.Author.Single(d => d.FirstName == "Џејн" && d.LastName == "Остин").AuthorID,
                           Title = "Гордост и предрасуда",
                           OriginalTitle = "Pride and prejudice",
                           Genre = "Новела",
                           Synopsis = "Приказната го следи животот на главниот карактер Елизабет Бенет, која се соочува со прашањето на манирите, воспитанието, моралот, етиката, образованието и бракот во англиското општество на крајот на 19 век. Гордост и предрасуда е сè уште интересна за модерните читатели и се наоѓа на врвот на листите за најчитани книги.",
                           NumberofPages = 348,
                           Price = 350,
                           ReleaseDate = DateTime.Parse("1813-1-28"),
                           Publisher = "Арс ЛИБРИС"
                       },
                    new Book
                    {
                        Picture = "denicija.png",
                        Authorid = context.Author.Single(d => d.FirstName == "Петре" && d.LastName == "М. Андреевски").AuthorID,
                        Title = "Дениција",
                        OriginalTitle = "Дениција",
                        Genre = "Поезија",
                        Synopsis = "Дениција е без сомнение една од најубавите македонски поетски збирки, а песната Кога ја љубев Дениција најубава творба напишана од великанот на македонската книжевност, Петре М. Андреевски. Првпат е објавена во 1968 година и истата година има добиено два награди - Браќа Миладиновци и 11 октомври, која во тоа време се доделуваше за едногодишна продукција.",
                        NumberofPages = 69,
                        Price = 200,
                        ReleaseDate = DateTime.Parse("1968-2-23"),
                        Publisher = "Три"
                    },
                    new Book
                    {
                        Picture = "maliotprinc.jpg",
                        Authorid = context.Author.Single(d => d.FirstName == "Антоан Де" && d.LastName == "Сент-Егзипери").AuthorID,
                        Title = "Малиот принц",
                        OriginalTitle = "Le petit prince",
                        Genre = "Класици",
                        Synopsis = "Погледнат низ детските очи, светот е енигма, често драматична. Затоа Малиот принц не престанува да поставува прашања за смислата на животот. Одговорите што ги добива од возрасните се бизарни со својот груб рационализам и ја покажуваат во вистинска светлина извештаченоста на светот на возрасните, па затоа авторот, усогласувајќи го својот светоглед со штедрата фантазија на детето, ја наоѓа волшебната формула за излез од агонијата на еден свет лишен од вистинските вредности. ",
                        NumberofPages = 104,
                        Price = 150,
                        ReleaseDate = DateTime.Parse("1943-4-6"),
                        Publisher = "Сакам книги"
                    }
                );
                context.SaveChanges();

                context.Movie.AddRange(
                    new Movie
                    {
                        Picture = "film.jpg",
                        Title = "Breakfast at Tiffany's - Појадок кај Тифани",
                        BookId = context.Book.Single(d => d.Title == "Појадок кај Тифани").BooksID,
                        ReleaseDate = DateTime.Parse("1961-10-5"),
                        Synopsis = "Одри Хепберн ја воодушевува публиката како безгрижната девојка Холи која низ Менхетен го бара милионерот од соништата за да се омажи. Џорџ Пепард го игра младиот писател кој е вовлечен во хаотичниот и волшебен животен стил на Холи.",
                        Trailer = "https://www.youtube.com/embed/hQ_O15kYlCI",
                        Rating = "7.7/10"
                    },
                    new Movie
                    {
                        Picture = "orientfilm.jpg",
                        Title = "Murder on the Orient Express - Убиство во Ориент Експрес",
                        BookId = context.Book.Single(d => d.Title == "Убиство во Ориент-Експрес").BooksID,
                        ReleaseDate = DateTime.Parse("2017-11-3"),
                        Synopsis = "По шокантното убиство на еден богат бизнисмен во луксузниот европски воз, кој патува кон запад среде зима, приватниот детектив Херкул Поаро мора да ја искористи целата своја умешност за да открие кој од патниците во возот е убиецот пред тој или таа повторно да нападне. Кенет Брана го режира филмот и ја предводи ѕвездената актерска екипа во која се Џони Деп, Мишел Фајфер, Пенелопи Крус и Џуди Денч, во овој напнат трилер врз основа на бестселер романот од Агата Кристи.",
                        Trailer = "https://www.youtube.com/embed/Mq4m3yAoW8E",
                        Rating = "6.5/10"
                    },
                    new Movie
                    {
                        Picture = "prideandprejudice.jpg",
                        Title = "Pride & Prejudice - Гордост и предрасуда",
                        BookId = context.Book.Single(d => d.Title == "Гордост и предрасуда").BooksID,
                        ReleaseDate = DateTime.Parse("2005-7-25"),
                        Synopsis = "Дејството на филмот се одвива во грегоријанска Англија. Госпоѓата Бенет ги одгледува своите ќерки: Џејн, Елизабет, Мери, Кити и Лидија, со намера да ги омажи за богат маж, кој со своето богатство би го издржувал целото семејство. Тие не се од висока класа, а нивната куќа во Хартфордиш ќе биде наследена од далечен роднина, ако умре господинот Бенет. Богатиот господин Бингли и неговиот најдобар пријател, господинот Дарси пристигнуваат во градот за да го поминат летото на еден имот што се наоѓа во близина. Тогаш животот на Елизабет потполно се менува.",
                        Trailer = "https://www.youtube.com/embed/UykqkIqRTU8",
                        Rating = "7.8/10"
                    },
                    new Movie
                    {
                        Picture = "thelittleprince.png",
                        Title = "The Little Prince - Малиот принц",
                        BookId = context.Book.Single(d => d.Title == "Малиот принц").BooksID,
                        ReleaseDate = DateTime.Parse("2015-7-29"),
                        Synopsis = "Од Марк Озборн пристигнува првата анимирана филмска адаптација на легендарното ремек-дело на Антоан де Сент Егзипери, „Малиот Принц“. Во фокусот на филмот е малото девојче чија мајка ја подготвува за светот на возрасните во којшто живеат, но во тоа ја прекинува ексцентричниот сосед со добро срце, Авијатичарот. Авијатичарот на својата нова пријателка ѝ го прикажува неверојатниот свет каде што сѐ е можно. Свет којшто и нему одамна му бил прикажан од страна на Малиот Принц. Тука започнува волшебното и емотивно патување на малото девојче во нејзината фантазија и во универзумот на Малиот Принц. Тука девојчето го открива своето детство и дознава дека сепак е најважна поврзаноста со луѓето и дека вистинската суштина може да се види само со срцето.",
                        Trailer = "https://www.youtube.com/embed/9gARHWfXE40",
                        Rating = "7.7/10"
                    }
                );
                context.SaveChanges();

               

            }
        }
    }
}
