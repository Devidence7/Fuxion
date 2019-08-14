﻿using Fuxion.Identity.Test.Dao;
using Fuxion.Identity.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Fuxion.Identity.Functions;
namespace Fuxion.Identity.Test
{
	public static class Context
	{
		public static void Initialize()
		{
			InitializeLocations();
			InitializeCategories();
			InitializeTags();
			InitializeGroups();
			InitializeIdentities();
			InitializePersons();
			InitializeDocuments();
			InitializePackages();
			InitializeAlbums();
		}
		public static void InitializeLocations()
		{
			// Create countries
			Countries.Usa = new CountryDao(nameof(Countries.Usa), nameof(Countries.Usa));
			Countries.Spain = new CountryDao(nameof(Countries.Spain), nameof(Countries.Spain));

			// Create states
			States.California = new StateDao(nameof(States.California), nameof(States.California), Countries.Usa);
			States.NewYork = new StateDao(nameof(States.NewYork), nameof(States.NewYork), Countries.Usa);
			States.Madrid = new StateDao(nameof(States.Madrid), nameof(States.Madrid), Countries.Spain);

			// Create cities
			Cities.SanFrancisco = new CityDao(nameof(Cities.SanFrancisco), nameof(Cities.SanFrancisco), States.California);
			Cities.Berkeley = new CityDao(nameof(Cities.Berkeley), nameof(Cities.Berkeley), States.California);
			Cities.NewYork = new CityDao(nameof(Cities.NewYork), nameof(Cities.NewYork), States.NewYork);
			Cities.Buffalo = new CityDao(nameof(Cities.Buffalo), nameof(Cities.Buffalo), States.NewYork);
			Cities.Madrid = new CityDao(nameof(Cities.Madrid), nameof(Cities.Madrid), States.Madrid);
			Cities.Alcorcon = new CityDao(nameof(Cities.Alcorcon), nameof(Cities.Alcorcon), States.Madrid);

			// Initialize countries
			Countries.Usa.States = new[] { States.California, States.NewYork };
			Countries.Spain.States = new[] { States.Madrid };

			// Initialize states
			States.California.Cities = new[] { Cities.SanFrancisco, Cities.Berkeley };
			States.NewYork.Cities = new[] { Cities.NewYork, Cities.Buffalo };
			States.Madrid.Cities = new[] { Cities.Madrid, Cities.Alcorcon };
		}
		public static void InitializeCategories()
		{
			// Create categories
			Categories.Sales = new CategoryDao(nameof(Categories.Sales), nameof(Categories.Sales));
			Categories.Marketing = new CategoryDao(nameof(Categories.Marketing), nameof(Categories.Marketing));
			Categories.Commercial = new CategoryDao(nameof(Categories.Commercial), nameof(Categories.Commercial));
			Categories.Purchases = new CategoryDao(nameof(Categories.Purchases), nameof(Categories.Purchases));

			// Initialize categories
			Categories.Sales.Children = new[] { Categories.Commercial, Categories.Marketing };
			Categories.Commercial.Parent = Categories.Sales;
			Categories.Marketing.Parent = Categories.Sales;
		}
		public static void InitializeTags()
		{
			// Create categories
			Tags.Urgent = new TagDao(nameof(Tags.Urgent), nameof(Tags.Urgent));
			Tags.Important = new TagDao(nameof(Tags.Important), nameof(Tags.Important));
		}
		public static void InitializeGroups()
		{
			// Create groups
			Groups.Admins = new GroupDao(nameof(Groups.Admins), nameof(Groups.Admins));
		}
		public static void InitializeIdentities()
		{
			// Create identities
			new PasswordProviderMock().Generate("root", out var salt, out var hash);
			Identities.Root = new IdentityDao(nameof(Identities.Root), nameof(Identities.Root), nameof(Identities.Root), hash, salt);
			Identities.Customer = new IdentityDao(nameof(Identities.Customer), nameof(Identities.Customer), nameof(Identities.Customer), hash, salt);
			Identities.FilmManager = new IdentityDao(nameof(Identities.FilmManager), nameof(Identities.FilmManager), nameof(Identities.FilmManager), hash, salt);

			// Initialize identities
			Identities.Root.Groups = new[] { Groups.Admins };
			Identities.Root.Permissions = new[]
			{
				new PermissionDao("", "", Identities.Root, Admin.Id.ToString() ?? "")
				{
					Value = true,
				}
			};
			Identities.Customer.Category = Categories.Purchases;
			Identities.Customer.CategoryId = Categories.Purchases.Id;
		}
		public static void InitializePersons()
		{
			// Create persons
			Persons.Admin = new PersonDao(nameof(Persons.Admin), nameof(Persons.Admin));
			Persons.MadridAdmin = new PersonDao(nameof(Persons.MadridAdmin), nameof(Persons.MadridAdmin));
			Persons.AlcorconAdmin = new PersonDao(nameof(Persons.AlcorconAdmin), nameof(Persons.AlcorconAdmin));

			// Initialize persons
			Persons.MadridAdmin.City = Cities.Madrid;
			Persons.AlcorconAdmin.City = Cities.Alcorcon;
		}
		public static void InitializeDocuments()
		{
			// Create documents
			Documents.Word1 = new WordDocumentDao(nameof(Documents.Word1), nameof(Documents.Word1));
		}
		public static void InitializePackages()
		{
			// Create packages
			Packages.Package1 = new PackageDao(nameof(Packages.Package1), nameof(Packages.Package1));
		}
		public static void InitializeSongs() { }
		public static void InitializeAlbums()
		{
			// Create packages
			Albums.Album1 = new AlbumDao(nameof(Albums.Album1), nameof(Albums.Album1));
		}
	}
	#region Locations
	public static class Countries
	{
		public static CountryDao Usa { get; set; }
		public static CountryDao Spain { get; set; }
	}
	public static class States
	{
		public static StateDao California { get; set; }
		public static StateDao NewYork { get; set; }
		public static StateDao Madrid { get; set; }
	}
	public static class Cities
	{
		public static CityDao SanFrancisco { get; set; }
		public static CityDao Berkeley { get; set; }
		public static CityDao NewYork { get; set; }
		public static CityDao Buffalo { get; set; }
		public static CityDao Madrid { get; set; }
		public static CityDao Alcorcon { get; set; }
	}
	#endregion
	#region Categories
	public static class Categories
	{
		public static CategoryDao Sales { get; set; }
		public static CategoryDao Marketing { get; set; }
		public static CategoryDao Commercial { get; set; }
		public static CategoryDao Purchases { get; set; }
	}
	#endregion
	#region Tags
	public static class Tags
	{
		public static TagDao Urgent { get; set; }
		public static TagDao Important { get; set; }
	}
	#endregion
	#region Groups
	public static class Groups
	{
		public static IEnumerable<GroupDao> All => new[] { Admins };
		public static GroupDao Admins { get; set; }
	}
	#endregion
	#region Identities
	public static class Identities
	{
		public static IEnumerable<IdentityDao> All => new[] { Root, Customer, FilmManager };
		public static IdentityDao Root { get; set; }
		public static IdentityDao Customer { get; set; }
		public static IdentityDao FilmManager { get; set; }
	}
	#endregion
	#region Persons
	public static class Persons
	{
		public static PersonDao Admin { get; set; }
		public static PersonDao MadridAdmin { get; set; }
		public static PersonDao AlcorconAdmin { get; set; }
	}
	#endregion
	#region Documents
	public static class Documents
	{
		public static IEnumerable<DocumentDao> All => new[] { Word1 };
		public static WordDocumentDao Word1 { get; set; }
	}
	#endregion
	#region Packages
	public static class Packages
	{
		public static IEnumerable<PackageDao> All => new[] { Package1 };
		public static PackageDao Package1 { get; set; }
	}
	#endregion
	#region Songs
	public static class Songs
	{
		public static IEnumerable<SongDao> All => new SongDao[] { };
	}
	#endregion
	#region Albums
	public static class Albums
	{
		public static IEnumerable<AlbumDao> All => new[] { Album1 };
		public static AlbumDao Album1 { get; set; }
	}
	#endregion
}
