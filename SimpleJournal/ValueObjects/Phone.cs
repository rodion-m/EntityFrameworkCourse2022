using System;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace SimpleJournal.ValueObjects;

[Owned]
public class Phone
{
    public string Value { get; private set; }

    private static void Example()
    {
        var phone1 = new Phone("+79311111111");
        var phone2 = new Phone("+79311111111");
        if (phone1.Equals(phone2))
        {
            //...
        }
    }
    
#pragma warning disable CS8618
    protected Phone() {}
#pragma warning restore CS8618
    public Phone(string value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }
        if (!IsPhone(value))
        {
            throw new InvalidOperationException("Некорретный номер телефона");
        }
        Value = value;
    }

    public override string ToString() => Value;
    
    private static bool IsPhone(string value)
    {
        var ruPhoneRegex = new Regex(@"\+79\d{2}\d{7}");
        return ruPhoneRegex.IsMatch(value);
    }
    
    protected bool Equals(Phone other)
    {
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Phone)obj);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static bool operator ==(Phone? left, Phone? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Phone? left, Phone? right)
    {
        return !Equals(left, right);
    }
}