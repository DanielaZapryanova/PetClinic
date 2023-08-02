﻿using PetClinic.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace PetClinic.Models
{
    public class AddPetViewModel
    {
        [Required]
        [MaxLength(20)]
        public string? Name { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string? Breed { get; set; }

        [Required]
        public string? Gender { get; set; }

        [Required]
        public string? Color { get; set; }

        [Required]
        public decimal Weight { get; set; }

        public string? Image { get; set; }

        public string? ImagePath { get; set; }

        [Required]
        public int OwnerId { get; set; }

        public IList<OwnerViewModel>? PossibleOwners { get; set; }
    }
}
