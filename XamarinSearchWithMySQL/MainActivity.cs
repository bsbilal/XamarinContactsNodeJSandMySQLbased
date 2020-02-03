using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Refit;
using XamarinSearchWithMySQL.API;
using Com.Miguelcatalan.Materialsearchview;
using Android.Views;
using XamarinSearchWithMySQL.Adapter;
using Android.Support.V7.Widget;
using static Com.Miguelcatalan.Materialsearchview.MaterialSearchView;
using System;
using System.Collections.Generic;

namespace XamarinSearchWithMySQL
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity,IOnQueryTextListener,ISearchViewListener
    {
        ISearchAPI myAPI;
        MaterialSearchView searchView;

        LinearLayoutManager layoutManager;
        PersonAdapter adapter;    
        RecyclerView recyclerSearch;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            searchView =FindViewById<MaterialSearchView>(Resource.Id.searchView);
            searchView.SetOnQueryTextListener(this);
            searchView.SetOnSearchViewListener(this);
        


            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            toolbar.Title = "Search";
            SetSupportActionBar(toolbar);

            myAPI = GetAPI();

            //view
            recyclerSearch = FindViewById<RecyclerView>(Resource.Id.recyclerSearch);
            layoutManager = new LinearLayoutManager(this);
            recyclerSearch.HasFixedSize = true;
            recyclerSearch.SetLayoutManager(layoutManager);
            recyclerSearch.AddItemDecoration(new DividerItemDecoration(this, layoutManager.Orientation));

            //ilk açıılış

            RunOnUiThread(() => { ShowAllPerson(); });

        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main_menu, menu);
            var item = menu.FindItem(Resource.Id.actionSearch);
            searchView.SetMenuItem(item);
            return true;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        private ISearchAPI GetAPI()
        {
            return RestService.For<ISearchAPI>("http://10.0.2.2:3000");//10.0.2.2 localhost
        }

        public bool OnQueryTextChange(string p0)
        {
            return false;

        }

        public bool OnQueryTextSubmit(string p0)
        {
            StartSearch(p0);
                return true;
        }

        private async void StartSearch(string nameSearch)
        {
            //CREATE POST 
            try
            {
                Dictionary<string, object> data_post = new Dictionary<string, object>();
                data_post.Add("search", nameSearch);

                var personResult = await myAPI.SearchPerson(data_post);


                if (personResult.Count > 0)
                    adapter = new PersonAdapter(this, personResult);
                recyclerSearch.SetAdapter(adapter);
            }
            catch (Exception ex)
            {

                Toast.MakeText(this, "User has not found", ToastLength.Short).Show();
            }
         

        }

        public void OnSearchViewClosed()
        {
            //search view closed > show all person 
            ShowAllPerson();

        }

        private async void ShowAllPerson()
        {
           
             var contacts = await myAPI.GetPeople();
                adapter= new PersonAdapter(this, contacts);

            adapter.NotifyDataSetChanged();
            recyclerSearch.SetAdapter(adapter);
        }
        public override void OnBackPressed()
        {
            ShowAllPerson();
        }
        public void OnSearchViewShown()
        {
            //throw new System.NotImplementedException();
        }
    }
}