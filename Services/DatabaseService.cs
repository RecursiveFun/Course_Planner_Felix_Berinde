using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Course_Planner_Felix_Berinde.Models;
using SQLite;
using Xamarin.Essentials;


namespace Course_Planner_Felix_Berinde.Services
{
    public static class DatabaseService
    {
        private static SQLiteAsyncConnection _db;
        private static SQLiteConnection _dbConnection;

        static async Task Init()
        {
            if (_db != null) //don't create db if it already exists
            {
                return;
            }

            //Get an absolute path to the database file.
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "Gadgets.db");

            _db = new SQLiteAsyncConnection(databasePath);
            _dbConnection = new SQLiteConnection(databasePath);

            await _db.CreateTableAsync<Gadget>();
            await _db.CreateTableAsync<Widget>();
        }


        #region Gadgets methods

        public static async Task AddGadget(string name, string color, int inStock, decimal price, DateTime creationDate)
        {
            await Init();
            var gadget = new Gadget()
            {
                Name = name,
                Color = color,
                InStock = inStock,
                Price = price,
                CreationDate = creationDate
            };
        }

        public static async Task RemoveGadget(int id)
        {
            await Init();
            await _db.DeleteAsync<Gadget>(id);
        }

        public static async Task<IEnumerable<Gadget>> GetGadgets()
        {
            await Init();

            var gadgets = await _db.Table<Gadget>().ToListAsync();
            return gadgets;
        }

        public static async Task UpdateGadget(int id, string name, string color, int inStock, decimal price, DateTime creationDate)
        {
            await Init();

            var gadgetQuery = await _db.Table<Gadget>().Where(i => i.Id == id).FirstOrDefaultAsync();

            if (gadgetQuery != null)
            {
                gadgetQuery.Name = name;
                gadgetQuery.Color = color;
                gadgetQuery.InStock = inStock;
                gadgetQuery.Price = price;
                gadgetQuery.CreationDate = creationDate;

            }
        }

        #endregion

        #region Widgets methods

        public static async Task AddWidget(int gadgetId, string name, string color, int inStock, decimal price,
            DateTime creationDate, bool notificationStart, string notes)
        {
            await Init();
            var widget = new Widget()
            {
                GadgetId = gadgetId,
                Name = name,
                Color = color, 
                InStock = inStock,
                Price = price,
                CreationDate = creationDate,
                StartNotification = notificationStart,
                Notes = notes
            };

            await _db.InsertAsync(widget);

            var id = widget.Id;
        }

        public static async Task RemoveWidget(int id)
        {
            await Init();
            await _db.DeleteAsync(id);
        }

        /* Method that retrieves Widgets for a given gadget based on the GadgetID relationship
           Used with CollectionView for showingWidgets related to a specific Gadget. */
        public static async Task<IEnumerable<Widget>> GetWidgets(int gadgetId)
        {
            await Init();

            var widgets = await _db.Table<Widget>().Where(i => i.GadgetId == gadgetId).ToListAsync();

            return widgets;
        }


        /*Used with notifications.
          See codebehind of Dashboard.xaml.cs for example. Using the onAppearing method is key.*/
        public static async Task<IEnumerable<Widget>> GetWidgets()
        {
            await Init();
            var widgets = await _db.Table<Widget>().ToListAsync();

            return widgets;
        }

        public static async Task UpdateWidget(int id, string name, string color, int inStock, decimal price,
            DateTime creationDate, bool notificationStart, string notes)
        {
            await Init();

            var widgetQuery = await _db.Table<Widget>().Where(i => i.Id == id).FirstOrDefaultAsync();

            if (widgetQuery != null)
            {
                widgetQuery.Name = name;
                widgetQuery.Color = color;
                widgetQuery.InStock = inStock;
                widgetQuery.Price = price;
                widgetQuery.StartNotification = notificationStart;
                widgetQuery.Notes = notes;
                widgetQuery.CreationDate = creationDate;

                await _db.UpdateAsync(widgetQuery);
            }
        }

        #endregion

        #region DemoData

        public static async void LoadSampleData()
        {
            await Init();

            Gadget gadget = new Gadget
            {
                Name = "Gadget 1",
                Color = "Blue",
                InStock = 255,
                Price = 25m,
                CreationDate = DateTime.Today.Date
            };
            await _db.InsertAsync(gadget);


            Widget widget = new Widget
            {
                Name = "Widget 1",
                Color = "Teal",
                InStock = 25,
                Price = 22.59M,
                CreationDate = DateTime.Now.Date,
                StartNotification = true,
                GadgetId = gadget.Id
            };
            await _db.InsertAsync(widget);


            Widget widget2 = new Widget
            {
                Name = "Widget 2",
                Color = "Green",
                InStock = 55,
                Price = 2.59M,
                CreationDate = DateTime.Now.Date,
                StartNotification = true,
                GadgetId = gadget.Id
            };
            await _db.InsertAsync(widget2);


            Gadget gadget2 = new Gadget
            {
                Name = "Gadget 2",
                Color = "Black",
                InStock = 155,
                Price = 250m,
                CreationDate = DateTime.Today.Date
            };
            await _db.InsertAsync(gadget2);


            Widget widget3 = new Widget
            {
                Name = "Widget 3",
                Color = "Teal",
                InStock = 25,
                Price = 22.59M,
                CreationDate = DateTime.Now.Date,
                StartNotification = true,
                GadgetId = gadget2.Id
            };
            await _db.InsertAsync(widget3);


            Widget widget4 = new Widget
            {
                Name = "Widget 4",
                Color = "Green",
                InStock = 55,
                Price = 2.59M,
                CreationDate = DateTime.Now.Date,
                StartNotification = true,
                GadgetId = gadget2.Id
            };
            await _db.InsertAsync(widget4);


            Widget widget5 = new Widget
            {
                Name = "Widget 5",
                Color = "Orange",
                InStock = 55,
                Price = 2.59M,
                CreationDate = DateTime.Now.Date,
                StartNotification = false,
                GadgetId = gadget2.Id
            };
            await _db.InsertAsync(widget5);
        }

        public static async Task ClearSampleData()
        {
            await Init();

            await _db.DropTableAsync<Widget>();
            await _db.DropTableAsync<Gadget>();
            _db = null;
            _dbConnection = null;
        }

        #endregion

        #region Count Determinations

        public static async Task<int> GetWidgetCountAsync(int selectedGadgetId)
        {
            int widgetCount = await _db.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM Widget where GadgetId = ?", selectedGadgetId);

            return widgetCount;
        }

        #endregion

    }
}
