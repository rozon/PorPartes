﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElCarro.Web.Models
{
    public class CreateVehiclePart
    {
        public CreateVehiclePart()
        {
            Models = new List<SelectListItem>();
            Makes = new List<SelectListItem>();
            Stores = new List<SelectListItem>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Model { get; set; }
        [Required]
        public int Make { get; set; }
        [Required]
        public int Store { get; set; }

        public IEnumerable<SelectListItem> Makes { get; set; }
        public IEnumerable<SelectListItem> Models { get; set; }
        public IEnumerable<SelectListItem> Stores { get; set; }


        public HttpPostedFileBase Photo { get; set; }

        private void FillMakes(IEnumerable<Make> makes)
        {
            var data = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Text="Select a Make",
                    Value = null,
                    Selected = Make == 0,
                }
            };
            if (makes != null)
                data.AddRange(makes.Select(m => new SelectListItem()
                {
                    Text = m.Name,
                    Value = m.Id.ToString(),
                    Selected = Make != 0 ? m.Id == Make : false,
                }));
            Makes = data;
        }

        private void FillModels(IQueryable<Model> models)
        {
            var data = new List<SelectListItem>()
            {
                //The default option, no selectable.
                new SelectListItem
                {
                    Text = "Select a Model",
                    Value = null,
                    Selected = Model == 0,
                }
            };
            if (Make != 0)
                data.AddRange(models.Where(m => m.Make.Id == Make).Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString(),
                    Selected = Model != 0 ? s.Id == Model : false,
                }).ToList());

            Models = data;
        }

        private void FillStores(IEnumerable<Store> stores)
        {
            var data = new List<SelectListItem>()
            {
                //The default option, no selectable.
                new SelectListItem
                {
                    Text="Select a Store",
                    Value = null,
                    Selected = Store == 0,
                }
            };
            if (stores != null)
                data.AddRange(stores.Select(s => new SelectListItem()
                {
                    Text = s.Name,
                    Value = s.StoreID.ToString(),
                    Selected = Store != 0 ? s.StoreID == Store : false,
                }));

            Stores = data;
        }

        public void FillDropDowns(IEnumerable<Make> makes,
            IEnumerable<Store> stores,
            IQueryable<Model> models)
        {
            FillMakes(makes);
            FillStores(stores);
            FillModels(models);
        }

        public static CreateVehiclePart Factory(IEnumerable<Make> makes,
            IEnumerable<Store> stores,
            IQueryable<Model> models,
            VehiclePart vehiclePart)
        {
            var model = new CreateVehiclePart();
            if (vehiclePart != null)
            {
                model.Name = vehiclePart.Name;
                model.Description = vehiclePart.Description;
                model.Make = vehiclePart.Model.Make.Id;
                model.Model = vehiclePart.Model.Id;
                model.Store = vehiclePart.Store.StoreID;
            }

            model.FillMakes(makes);
            model.FillStores(stores);
            model.FillModels(models);

            return model;
        }
    }
}