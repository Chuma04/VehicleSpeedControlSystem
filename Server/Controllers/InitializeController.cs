namespace VehicleSpeedControlSystem.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class InitializeController : ControllerBase{
        private readonly DataContext _context;
        private readonly List<List<Coordinate>> coordinates;
        // private List<TitleDeed> titleDeeds = new List<TitleDeed>();
        // private List<Landlord> landlords = new List<Landlord>();
        // private List<Purchase> purchases = new();

        public InitializeController(DataContext context)
        {
            _context = context;

            //         // Add some sample data
            // landlords.Add(new Landlord( "Mwila", "Mwamba", "mwila.mwamba@example.com", "123456", "Mwila Mwamba", "12 Independence Avenue, Lusaka", "+260 97 1234 567"));
            // landlords.Add(new Landlord( "Chipo", "Chanda", "chipo.chanda@example.com", "234567", "Chipo Chanda", "34 Cairo Road, Ndola", "+260 97 2345 678"));
            // landlords.Add(new Landlord( "Kunda", "Kapasa", "kunda.kapasa@example.com", "345678", "Kunda Kapasa", "56 Freedom Way, Kitwe", "+260 97 3456 789"));
            // landlords.Add(new Landlord( "Nasilele", "Nalumino", "nasilele.nalumino@example.com", "456789", "Nasilele Nalumino", "78 Lumumba Road, Livingstone", "+260 97 4567 890"));
            // landlords.Add(new Landlord( "Sipho", "Sikazwe", "sipho.sikazwe@example.com", "567890", "Sipho Sikazwe", "90 Great East Road, Chipata", "+260 97 5678 901"));
            // landlords.Add(new Landlord( "Mwape", "Mwale", "mwape.mwale@example.com", "678901", "Mwape Mwale", "12 Kabwe Road, Kabwe", "+260 97 6789 012"));
            // landlords.Add(new Landlord( "Namukolo", "Nkandu", "namukolo.nkandu@example.com", "789012","Namukolo Nkandu","34 Kafue Road, Kafue","+260 97 7890 123"));
            // landlords.Add(new Landlord("Chanda","Chileshe","chanda.chileshe@example.com","890123","Chanda Chileshe","56 Luanshya Road,Luanshya","+260 97 8901 234"));
            // landlords.Add(new Landlord("Lungu","Lungu","lungu.lungu@example.com","901234","Lungu Lungu","78 Chingola Road,Chingola","+260 97 9012 345"));
            // landlords.Add(new Landlord("Musonda","Mulenga","musonda.mulenga@example.com","012345","Musonda Mulenga","90 Solwezi Road,Solwezi","+260 97 0123 456"));
            // landlords.Add(new Landlord("Nchimunya","Ng'andu","nchimunya.ngandu@example.com","123450","Nchimunya Ng'andu","12 Mongu Road,Mongu","+260 97 1234 567"));
            // landlords.Add(new Landlord("Sibeso","Simasiku","sibeso.simasiku@example.com","234561","Sibeso Simasiku","34 Sesheke Road,Sesheke","+260 97 2345 678"));
            // landlords.Add(new Landlord("Tembo","Tembo","tembo.tembo@example.com","345672","Tembo Tembo","56 Kasama Road,Kasama","+260 97 3456 789"));
            // landlords.Add(new Landlord("Wamunyima","Wamulume","wamunyima.wamulume@example.com","456783","Wamunyima Wamulume","78 Mansa Road,Mansa","+260 97 4567 890"));
            // landlords.Add(new Landlord("Zulu","Zulu","zulu.zulu@example.com","567894","Zulu Zulu","90 Mpika Road,Mpika","+260 97 5678 901"));
            // landlords.Add(new Landlord("Bwalya","Banda","bwalya.banda@example.com","678905" ,"Bwalya Banda" ,"12 Choma Road,Choma" ,"+260-97-6789-012" ));
            // landlords.Add(new Landlord("Kaluba" ,"Kaluba" ,"kaluba.kaluba@example.com" ,"789016" ,"Kaluba Kaluba" ,"34 Mazabuka Road,Mazabuka" ,"+260-97-7890-123" ));
            // landlords.Add(new Landlord("Mukuka" ,"Mukuka" ,"mukuka.mukuka@example.com" ,"890127" ,"Mukuka Mukuka" ,"56 Monze Road,Monze" ,"+260-97-8901-234" ));
            // landlords.Add(new Landlord("Nalwimba" ,"Nalwimba" ,"nalwimba.nalwimba@example.com" ,"901238" ,"Nalwimba Nalwimba" ,"78 Petauke Road,Petauke" ,"+260-97-9012-345" ));
            // landlords.Add(new Landlord("Sikota" ,"Sikota" ,"sikota.sikota@example.com" ,"012349" ,"Sikota Sikota" ,"90 Mfuwe Road,Mfuwe" ,"+260-97-0123-456" ));
            // landlords.Add(new Landlord("Chisanga","Chisanga","chisanga.chisanga@example.com","123458","Chisanga Chisanga","12 Kaoma Road,Kaoma","+260 97 1234 567"));
            // landlords.Add(new Landlord("Kapembwa","Kapembwa","kapembwa.kapembwa@example.com","234569","Kapembwa Kapembwa","34 Senanga Road,Senanga","+260 97 2345 678"));
            // landlords.Add(new Landlord("Mumba","Mumba","mumba.mumba@example.com","345670","Mumba Mumba","56 Kalabo Road,Kalabo","+260 97 3456 789"));
            // landlords.Add(new Landlord("Nawa","Nawa","nawa.nawa@example.com","456781","Nawa Nawa","78 Mongu Road,Mongu","+260 97 4567 890"));
            // landlords.Add(new Landlord("Sitali","Sitali","sitali.sitali@example.com","567892","Sitali Sitali", "90 Sesheke Road,Sesheke","+260 97 5678 901"));
            // landlords.Add(new Landlord("Chibale", "Chibale", "chibale.chibale@example.com", "678903", "Chibale Chibale", "12 Samfya Road, Samfya", "+260 97 6789 012"));
            // landlords.Add(new Landlord( "Kasongo", "Kasongo", "kasongo.kasongo@example.com", "789014", "Kasongo Kasongo", "34 Kawambwa Road, Kawambwa", "+260 97 7890 123"));
            // landlords.Add(new Landlord( "Mwenya", "Mwenya", "mwenya.mwenya@example.com", "890125", "Mwenya Mwenya", "56 Mpulungu Road, Mpulungu", "+260 97 8901 234"));
            // landlords.Add(new Landlord( "Nkole", "Nkole", "nkole.nkole@example.com", "901236", "Nkole Nkole", "78 Mbala Road, Mbala", "+260 97 9012 345"));
            // landlords.Add(new Landlord("Soko", "Soko", "soko.soko@example.com", "012347", "Soko Soko", "90 Nakonde Road, Nakonde", "+260 97 0123 456"));

            
            
    
                coordinates = new List<List<Coordinate>>();
                var topLeft = new Coordinate(-12.793267583064663, 28.239256039524278);
                var topRight = new Coordinate(-12.793021242420101, 28.23467294507402);
                var bottomLeft = new Coordinate(-12.801009595030193, 28.2365134003257);
                var bottomRight = new Coordinate(-12.798827779342153, 28.241962591364985);
        
                var width = topRight.Longitude - topLeft.Longitude;
                var height = bottomLeft.Latitude - topLeft.Latitude;
        
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        var landCoordinates = new List<Coordinate>();
                        landCoordinates.Add(new Coordinate(topLeft.Latitude + i * height / 5, topLeft.Longitude + j * width / 6));
                        landCoordinates.Add(new Coordinate(topLeft.Latitude + i * height / 5, topLeft.Longitude + (j + 1) * width / 6));
                        landCoordinates.Add(new Coordinate(topLeft.Latitude + (i + 1) * height / 5, topLeft.Longitude + (j + 1) * width / 6));
                        landCoordinates.Add(new Coordinate(topLeft.Latitude + (i + 1) * height / 5, topLeft.Longitude + j * width / 6));
                        coordinates.Add(landCoordinates);
                    }
                }
        
                // Print the generated coordinates
                for (int i = 0; i < coordinates.Count; i++)
                {
                    Console.WriteLine("RestrictedArea " + (i+1) + ":");
                    foreach (var coordinate in coordinates[i])
                    {
                        Console.WriteLine(coordinate.Latitude + ", " + coordinate.Longitude);
                    }
                    Console.WriteLine();
                }
                
                // purchases = new List<Purchase>()
                // {
                //     new Purchase() { Date = new DateTime(2023, 1, 15), Price = 100.50 },
                //     new Purchase() { Date = new DateTime(2023, 2, 10), Price = 75.25 },
                //     new Purchase() { Date = new DateTime(2023, 3, 5), Price = 120.00 },
                //     new Purchase() { Date = new DateTime(2023, 4, 20), Price = 90.75 },
                //     new Purchase() { Date = new DateTime(2023, 5, 15), Price = 80.50 },
                //     new Purchase() { Date = new DateTime(2023, 6, 10), Price = 95.00 },
                //     new Purchase() { Date = new DateTime(2023, 7, 5), Price = 85.25 },
                //     new Purchase() { Date = new DateTime(2023, 8, 20), Price = 110.50 },
                //     new Purchase() { Date = new DateTime(2023, 9, 15), Price = 105.00 },
                //     new Purchase() { Date = new DateTime(2023, 10, 10), Price = 100.25 },
                //     new Purchase() { Date = new DateTime(2023, 11, 5), Price = 115.50 },
                //     new Purchase() { Date = new DateTime(2023, 12, 20), Price = 125.00 },
                //     new Purchase() { Date = new DateTime(2022, 1, 15), Price = 105.50 },
                //     new Purchase() { Date = new DateTime(2022, 2, 10), Price = 80.25 },
                //     new Purchase() { Date = new DateTime(2022, 3, 5), Price = 125.00 },
                //     new Purchase() { Date = new DateTime(2022, 4, 20), Price = 95.75 },
                //     new Purchase() { Date = new DateTime(2022, 5, 15), Price = 85.50 },
                //     new Purchase() { Date = new DateTime(2022, 6, 10), Price = 100.00 },
                //     new Purchase() { Date = new DateTime(2022, 7, 5), Price = 90.25 },
                //     new Purchase() { Date = new DateTime(2022, 8, 20), Price = 115.50 },
                //     new Purchase() { Date = new DateTime(2022, 9, 15), Price = 110.00 },
                //     new Purchase() { Date = new DateTime(2022, 10, 10), Price = 105.25 },
                //     new Purchase() { Date = new DateTime(2022, 11, 5), Price = 120.50 },
                //     new Purchase() { Date = new DateTime(2022,12 ,20 ),Price=130.00},
                //     new Purchase() { Date = new DateTime(2021,1 ,15 ),Price=95.50},
                //     new Purchase() { Date = new DateTime(2021,2 ,10 ),Price=70.25},
                //     new Purchase() { Date = new DateTime(2021,3 ,5 ),Price=115.00},
                //     new Purchase() { Date = new DateTime(2021,4 ,20 ),Price=85.75},
                //     new Purchase() { Date = new DateTime(2021,5 ,15 ),Price=75.50},
                //     new Purchase() { Date = new DateTime(2021,6 ,10 ),Price=90.00},
                //     new Purchase() { Date = new DateTime(2021,7 ,5 ),Price=80.25},
                //     new Purchase() { Date = new DateTime(2021,8 ,20 ),Price=105.50},
                //     new Purchase() { Date = new DateTime(2021,9 ,15 ),Price=100.00},
                //     new Purchase() { Date = new DateTime(2021,10 ,10 ),Price=95.25}
                //     // I generated the rest of the instances randomly
                //     // You can modify the values as you wish
                //     // I hope this helps you with your class
                //     // Have a nice day 😊
                // };
                
                
        
        // Add some sample data
        // titleDeeds.Add(new TitleDeed { Number = "TD-001", Date = new DateTime(2023, 1, 15), Location = "Lusaka", Size = "100 sqm", Type = "Residential" });
        // titleDeeds.Add(new TitleDeed { Number = "TD-002", Date = new DateTime(2023, 2, 10), Location = "Ndola", Size = "150 sqm", Type = "Commercial" });
        // titleDeeds.Add(new TitleDeed { Number = "TD-003", Date = new DateTime(2023, 3, 5), Location = "Livingstone", Size = "200 sqm", Type = "Industrial" });
        // titleDeeds.Add(new TitleDeed { Number = "TD-004", Date = new DateTime(2023, 4, 20), Location = "Kitwe", Size = "250 sqm", Type = "Agricultural" });
        // titleDeeds.Add(new TitleDeed { Number = "TD-005", Date = new DateTime(2023, 5, 25), Location = "Chipata", Size = "300 sqm", Type = "Residential" });
        // titleDeeds.Add(new TitleDeed { Number = "TD-006", Date = new DateTime(2023, 6, 30), Location = "Kabwe", Size = "350 sqm", Type = "Commercial" });
        // titleDeeds.Add(new TitleDeed { Number = "TD-007", Date = new DateTime(2023, 7, 15), Location = "Chingola", Size = "400 sqm", Type = "Industrial" });
        // titleDeeds.Add(new TitleDeed { Number = "TD-008", Date = new DateTime(2023, 8, 10), Location = "Solwezi", Size = "450 sqm", Type = "Agricultural" });
        // titleDeeds.Add(new TitleDeed { Number = "TD-009", Date = new DateTime(2023, 9, 5), Location = "Mongu", Size = "500 sqm", Type = "Residential" });
        // titleDeeds.Add(new TitleDeed { Number = "TD-010", Date = new DateTime(2023, 10, 20), Location = "Kasama", Size = "550 sqm", Type = "Commercial" });
        // titleDeeds.Add(new TitleDeed { Number = "TD-011", Date = new DateTime(2023, 11, 25), Location = "Mansa", Size = "600 sqm", Type = "Industrial" });
        // titleDeeds.Add(new TitleDeed { Number = "TD-012", Date = new DateTime(2023, 12, 30), Location = "Choma", Size = "650 sqm", Type = "Agricultural" });
        // titleDeeds.Add(new TitleDeed { Number = "TD-013", Date = new DateTime(2024, 1, 15), Location = "Lusaka", Size = "700 sqm", Type = "Residential" });
        // titleDeeds.Add(new TitleDeed { Number = "TD-014", Date = new DateTime(2024, 2, 10), Location = "Ndola", Size = "750 sqm", Type = "Commercial" });
        // titleDeeds.Add(new TitleDeed { Number = "TD-015", Date = new DateTime(2024, 3, 5), Location ="Livingstone",Size ="800 sqm",Type ="Industrial"});
        // titleDeeds.Add(new TitleDeed {Number ="TD-016",Date =new DateTime(2024,4,20),Location ="Kitwe",Size ="850 sqm",Type ="Agricultural"});
        // titleDeeds.Add(new TitleDeed{Number ="TD-017",Date =new DateTime(2024,5,25),Location ="Chipata",Size ="900 sqm",Type ="Residential"});
        // titleDeeds.Add(new TitleDeed{Number ="TD-018",Date =new DateTime(2024,6,30),Location ="Kabwe",Size ="950 sqm",Type ="Commercial"});
        // titleDeeds.Add(new TitleDeed{Number ="TD-019",Date =new DateTime(2024,7,15),Location ="Chingola",Size ="1000 sqm",Type ="Industrial"});
        // titleDeeds.Add(new TitleDeed{Number ="TD-020",Date =new DateTime(2024,8,10),Location ="Solwezi",Size ="1050 sqm",Type ="Agricultural"});
        // titleDeeds.Add(new TitleDeed{Number ="TD-021",Date =new DateTime(2024,9,5),Location ="Mongu",Size ="1100 sqm",Type ="Residential"});
        // titleDeeds.Add(new TitleDeed{Number ="TD-022",Date =new DateTime(2024,10,20),Location ="Kasama",Size ="1150 sqm",Type ="Commercial"});
        // titleDeeds.Add(new TitleDeed{Number ="TD-023",Date =new DateTime(2024,11,25),Location ="Mansa",Size ="1200 sqm",Type ="Industrial"});
        // titleDeeds.Add(new TitleDeed{Number ="TD-024",Date =new DateTime(2024,12,30),Location ="Choma",Size ="1250 sqm",Type ="Agricultural"});
        // titleDeeds.Add(new TitleDeed{Number ="TD-025",Date = new DateTime(2025, 1, 15), Location = "Lusaka", Size = "1300 sqm", Type = "Residential" });
        // titleDeeds.Add(new TitleDeed { Number = "TD-026", Date = new DateTime(2025, 2, 10), Location = "Ndola", Size = "1350 sqm", Type = "Commercial" });
        // titleDeeds.Add(new TitleDeed { Number = "TD-027", Date = new DateTime(2025, 3, 5), Location = "Livingstone", Size = "1400 sqm", Type = "Industrial" });
        // titleDeeds.Add(new TitleDeed { Number = "TD-028", Date = new DateTime(2025, 4, 20), Location = "Kitwe", Size = "1450 sqm", Type = "Agricultural" });
        // titleDeeds.Add(new TitleDeed { Number = "TD-029", Date = new DateTime(2025, 5, 25), Location = "Chipata", Size = "1500 sqm", Type = "Residential" });
        // titleDeeds.Add(new TitleDeed { Number = "TD-030", Date = new DateTime(2025, 6, 30), Location = "Kabwe", Size = "1550 sqm", Type = "Commercial" });
        //
        // // Print the list of title deeds
        // foreach (TitleDeed td in titleDeeds)
        // {
        //     Console.WriteLine("Number: {0}, Date: {1}, Location: {2}, Size: {3}, Type: {4}", td.Number, td.Date.ToShortDateString(), td.Location, td.Size, td.Type);
        // }

        
            
            
        } 


        [HttpGet("createland")]
        public ActionResult<List<RestrictedArea>> Get()
        {
            List<RestrictedArea> restrictedAreas = new List<RestrictedArea>();
            for (int i = 0; i < coordinates.Count; i++)
            {
                var land = new RestrictedArea();
                // land.IsReserved = false;
                // land.TitleDeed = titleDeeds[i];
                // land.Landlord = landlords[i];
                land.Perimeter = coordinates[i];
                land.Classification = Classification.School;
                // land.Purchase = purchases[i];
                // land.OwnershipHistory = new List<Ownership>();
                
                restrictedAreas.Add(land);
            }
            
            foreach( var land in restrictedAreas)
                _context.RestrictedAreas.Add(land);
            _context.SaveChanges();
            return Ok(restrictedAreas);
        }
        
   

  
    }
}
