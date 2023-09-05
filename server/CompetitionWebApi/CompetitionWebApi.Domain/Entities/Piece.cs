using CompetitionWebApi.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CompetitionWebApi.Domain.Entities;

[Owned]
public class Piece
{
    public string Name { get; set; }
    public string Composer { get; set; }
    public Period Period { get; set; }
}
