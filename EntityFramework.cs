using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace AshtonBro.Code
{
    /*
     <---------- SQL базы данных и Entity Framework в C#  ---------------->

    При изменении и добавлении новой таблицы в класс, необходимо разрешить миграцию и обновить базу данных
    Если мы работаем с базой данной в проекте единожды в Консоле диспетчера пакетов необходимо ввести команду enable-migrations
    Далее уже если мы постоянно добавляем или изменяем таблицы также в этом же диспетчере вводим команду add-migration AddBandCountry что является коммитом после команды add-migration
    далее вводим update-database.
     */
    public class EntityFramework
    {
        public static void RunDemo()
        {
            using (var context = new MyDbContext())
            {
                //Country = "Armenia"
                //Country = "USA"
                //Country = "Germany"

                var bands = new List<Band>
                {
                    new Band() { Name = "AC/DC", Year = 1976 },
                    new Band() { Name = "Omph", Year = 2002 },
                    new Band() { Name = "CORN", Year = 1995 }
                };

               // context.Bands.AddRange(bands);
                context.SaveChanges();

                var songs = new List<Song>
                {
                    new Song() { Name = "Toxicity", BandId = 16 },
                    new Song() { Name = "Smells like Teen Spirit", BandId = 17 },
                    new Song() { Name = "In bloom", BandId = 17 },
                    new Song() { Name = "Mutter", BandId = 18 }
                };

               // context.Songs.AddRange(songs);
                context.SaveChanges();

                //context.Bands.RemoveRange(context.Bands); очистить таблицу

                foreach (var song in songs)
                {
                    Console.WriteLine($"Songs: {song.Name}, Name: {song.Band?.Name}, Year: {song.Band.Year}");
                }

                Console.ReadLine();

            }
        }

        private class MyDbContext : DbContext
        {
        }
    }

    public class Song
    {
        public int SongId { get; set; }
        public string Name { get; set; }
        public int BandId { get; set; }

        public virtual Band Band { get; set; }
    }

    public class Band
    {
        public int BandId { get; set; }
        public string Name { get; set; }
        public int? Year { get; set; }

        public virtual ICollection<Song> Songs { get; set; }
    }
}

/*
 app.config

<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <configSections>
    <!-- For more information on Entity Framework configuration, visit http://go.microsoft.com/fwlink/?LinkID=237468 -->
    <section name="entityFramework"
       type="System.Data.Entity.Internal.ConfigFile.EntityFrameworkSection, EntityFramework, Version=4.3.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" />
  </configSections>
  
  <connectionStrings>
    <add name="DbConnectionString"
         connectionString="data source=ASHTON\ASHTON;initial catalog=MusicAlboms;integrated security=True;" 
         providerName="System.Data.SqlClient" />
  </connectionStrings>

  <entityFramework>
    
    <defaultConnectionFactory type="System.Data.Entity.Infrastructure.LocalDbConnectionFactory, EntityFramework">
      <parameters>
        <parameter value="mssqlocaldb" />
      </parameters>
    </defaultConnectionFactory>
    
    <providers>
      <provider invariantName="System.Data.SqlClient" type="System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer" />
    </providers>
    
  </entityFramework>
</configuration>

 */