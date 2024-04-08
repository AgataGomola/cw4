using System;

namespace LegacyApp.Interfaces;

public interface ICreditLimitService
{
    int GetCreditLimit(string LastName, DateTime DateOfBirth);
}