using FinancialOrganization.API.Communication.DTOs;

namespace FinancialOrganization.API.Communication.Response.Cards;

public class AllCards
{
    public List<CardDto> Items { get; set; } = [];
}
