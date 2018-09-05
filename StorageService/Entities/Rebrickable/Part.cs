using System;
using System.Collections.Generic;
using RestSharp.Deserializers;

namespace StorageServices.Entities.Rebrickable
{
    public class PartInstance
    {
        public int Quantity { get; set; }
        public Part Part { get; set; }
        public Color Color { get; set; }
    }

    public class Part
    {
        [DeserializeAs(Name="part_num")]
        public string PartNumber { get; set; }
        public string Name { get; set; }
        [DeserializeAs(Name = "part_cat_id")]
        public int PartCategoryId { get; set; }
        [DeserializeAs(Name = "part_url")]
        public string PartUrl { get; set; }
        [DeserializeAs(Name = "part_img_url")]
        public string PartImageUrl { get; set; }
        [DeserializeAs(Name = "external_ids")]
        public PartExternalId ExternalId { get; set; }
    }

    public class PartExternalId
    {
        public IEnumerable<string> BrickOwl { get; set; }
        public IEnumerable<string> Lego { get; set; }
        public IEnumerable<string> BrickLink { get; set; }
        public IEnumerable<string> LDraw { get; set; }
        public IEnumerable<string> Peeron { get; set; }
    }

    public class ColorExternalId
    {
        public string Name { get; set; }
        public ExternalIdInstance Lego { get; set; }
        public ExternalIdInstance BrickLink { get; set; }
        public ExternalIdInstance BrickOwl { get; set; }
        public ExternalIdInstance LDraw { get; set; }
        public ExternalIdInstance Peeron { get; set; }
    }

    public class ExternalIdInstance
    {
        [DeserializeAs(Name = "ext_descrs")]
        public IEnumerable<IEnumerable<string>> Descriptions { get; set; }
        [DeserializeAs(Name = "ext_ids")]
        public IEnumerable<int> Ids { get; set; }
    }

    public class Color
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RGB { get; set; }
        [DeserializeAs(Name = "is_trans")]
        public Boolean IsTransparent { get; set; }
        [DeserializeAs(Name = "external_ids")]
        public ColorExternalId ExternalIds { get; set; }
    }
}
