using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SimpleJournal;

[Owned]
public class Email
{
    public string Value { get; private set; }

    public Email(string value)
    {
        if (!IsValidEmail(value))
        {
            throw new InvalidOperationException("Email некорректен");
        }
        Value = value;
    }

    private bool IsValidEmail(string text)
    {
        var attribute = new EmailAddressAttribute();
        return attribute.IsValid(text);
    }
}

public class Tester
{
    void Test()
    {
        Email email = new Email("top@ya.tu");
       
    }
}