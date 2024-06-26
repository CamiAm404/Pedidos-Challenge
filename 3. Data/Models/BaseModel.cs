﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace _3._Data.Models;

public class BaseModel
{
    public int Id { get; set; }
    public int CreatedUser { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedDate  { get; set; }
    public DateTime? UpdatedDate  { get; set; }
    
    [DefaultValue(true)]
    public Boolean IsActive  { get; set; }
}