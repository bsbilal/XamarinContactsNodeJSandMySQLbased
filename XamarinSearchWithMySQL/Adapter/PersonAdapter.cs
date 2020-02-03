using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using XamarinSearchWithMySQL.Model;

namespace XamarinSearchWithMySQL.Adapter
{
    public class PersonAdapter : RecyclerView.Adapter
    {
        private Context context;
        private List<Model.Person> contacts;
        
        public PersonAdapter(Context context,List<Model.Person> people)
        {
            this.context = context;
            this.contacts = people;
        }

        public override int ItemCount => contacts.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            
            MyViewHolder myViewHolder = holder as MyViewHolder;
            myViewHolder.name.Text = contacts[position].Name;
            myViewHolder.adress.Text = contacts[position].Adress;
            myViewHolder.email.Text = contacts[position].EMail;
            myViewHolder.phone.Text = contacts[position].Phone;
            if (position % 2 != 0) 
                myViewHolder.rootView.SetBackgroundColor(Color.ParseColor("#E1E1E1"));
            else if (position % 2 == 0)       
                myViewHolder.rootView.SetBackgroundColor(Color.White);


        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.Person_Layout, parent, false);
            return new MyViewHolder(itemView);
        }

        private class MyViewHolder : RecyclerView.ViewHolder
        {
            public TextView name, adress, email, phone;
            public CardView rootView;
            public MyViewHolder(View itemView) : base(itemView)
            {
                rootView = itemView.FindViewById<CardView>(Resource.Id.rootView);
                name = itemView.FindViewById<TextView>(Resource.Id.name);
                adress = itemView.FindViewById<TextView>(Resource.Id.adress);
                email = itemView.FindViewById<TextView>(Resource.Id.email);
                 phone= itemView.FindViewById<TextView>(Resource.Id.phone);
            }
        }
    }

  
}