using BLLManage.Domain.Entities;
using BLLManage.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLManage.Tests.Entities;

public class CompanyTests
{
    [Fact]
    public void Should_Create_Company_When_Data_Is_Valid()
    {
        // Arrange & Act
        var company = new Company(
            "BLLManage",
            "contato@bllmanage.com",
            "(13)99999-9999");

        // Assert
        Assert.NotNull(company);
        Assert.Equal("BLLManage", company.Name);
        Assert.Equal("contato@bllmanage.com", company.Email);
        Assert.Equal("(13)99999-9999", company.Phone);
    }
    [Fact]
    public void Should_Throw_Exception_When_Name_Is_Empty()
    {
        Assert.Throws<DomainException>(() =>
            new Company("", "contato@bllmanage.com", "(13)99999-9999"));
    }

    [Fact]
    public void Should_Throw_Exception_When_Email_Is_Empty()
    {
        Assert.Throws<DomainException>(() =>
            new Company("BLLManage", "", "(13)99999-9999"));
    }

    [Fact]
    public void Should_Throw_Exception_When_Phone_Is_Empty()
    {
        Assert.Throws<DomainException>(() =>
            new Company("BLLManage", "contato@bllmanage.com", ""));
    }
}