using Credal.Net.Extensions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Credal.Net.Tests.Utils;

[TestFixture]
public class UriExtensionTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void UriExtension_CombinePathsSimpleString_ShouldReturnCombinedPath()
    {
        // Arrange
        string shouldReturn = "https://api.testuri.ai/api/testing/endpoint";

        // Act
        var uriString = new Uri("https://api.testuri.ai").Combine("api", "testing", "endpoint");

        // Assert
        uriString.Should().Be(shouldReturn);
    }

    [Test]
    public void UriExtension_CombinePathsComplexString_ShouldReturnCombinedPath()
    {
        // Arrange
        string shouldReturn = "https://api.testuri.ai/api/testing/endpoint";

        // Act
        var uriString = new Uri("https://api.testuri.ai").Combine("/api", "/testing", "/endpoint");

        // Assert
        uriString.Should().Be(shouldReturn);
    }
}
