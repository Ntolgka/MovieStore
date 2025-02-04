﻿using System.ComponentModel.DataAnnotations.Schema;
using MovieStore.Base;

namespace MovieStore.Data.Domain;

public class Order : BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public int MovieId { get; set; }
    public Movie Movie { get; set; }
    public decimal Price { get; set; }
    public DateTime PurchaseDate { get; set; }
}