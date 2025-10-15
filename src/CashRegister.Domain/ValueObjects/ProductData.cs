using System;

namespace CashRegister.Domain.ValueObjects;

public record ProductData(
    string Barcode,
    string Name,
    decimal Price
);
