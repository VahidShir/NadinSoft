﻿using PhoneNumbers;

using System.Net.Mail;

namespace NadinSoft.Domain;

public class Product
{
    public long Id { get; protected set; }

    /// <summary>
    /// UserName of the person who created the product
    /// </summary>
    public string CreatedBy { get; private set; }

    public string Name { get; private set; }

    public DateOnly ProduceDate { get; private set; }

    public string ManufacturePhone { get; private set; }

    public string ManufactureEmail { get; private set; }

    public bool IsAvailable { get; private set; }

    private Product() { }

    internal Product(string createdBy, string name, DateOnly produceDate, string manufacturePhone, string manufactureEmail, bool isAvailable)
    {
        
        ArgumentNullException.ThrowIfNullOrWhiteSpace(manufacturePhone);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(manufactureEmail);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(createdBy);

        ValidateName(name);
        ValidateEmail(manufactureEmail);
        ValidatePhone(manufacturePhone);
        ValidateProduceDate(produceDate);

        CreatedBy = createdBy;
        Name = name;
        ProduceDate = produceDate;
        ManufacturePhone = manufacturePhone;
        ManufactureEmail = manufactureEmail;
        IsAvailable = isAvailable;
    }

    private void ValidateName(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
    }

    private void ValidateEmail(string email)
    {
        bool isValid = MailAddress.TryCreate(email, out _);

        if (!isValid)
            throw new ArgumentException("Manufacture email is invalid", paramName: nameof(ManufactureEmail));
    }

    private void ValidatePhone(string phoneNumber)
    {
        var phoneNumberUtil = PhoneNumberUtil.GetInstance();

        PhoneNumber parsdPhoneNumber = null;

        try
        {
            parsdPhoneNumber = phoneNumberUtil.Parse(phoneNumber, "IR");
        }
        catch (Exception)
        {
            throw new ArgumentException("Manufacture phone number is invalid", paramName: nameof(ManufacturePhone));
        }

        bool isValid = phoneNumberUtil.IsValidNumber(parsdPhoneNumber);

        if (!isValid)
            throw new ArgumentException("Manufacture phone number is invalid", paramName: nameof(ManufacturePhone));
    }

    private void ValidateProduceDate(DateOnly produceDate)
    {
        bool isValid = produceDate < DateOnly.FromDateTime(DateTime.UtcNow);

        if (!isValid)
            throw new NadinSoftBusinessException("Product creation date is invalid.");
    }
}