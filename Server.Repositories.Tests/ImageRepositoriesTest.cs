using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage;
using Azure.Storage.Blobs;
using SETraining.Server.Repositories;
using SETraining.Shared;
using Xunit;

namespace Server.Repositories.Tests;

public class ImageRepositoriesTest
{
    private BlobContainerClient _client;
    private ImageRepository _repository;
    private string _mockUserName;
    private string _mockContainerName;
    
    public ImageRepositoriesTest()
    {
        //Account name
        _mockUserName = "devstoreaccount1";
        _mockContainerName = "devstoretest";
        
        // With account name and key
        _client = new BlobContainerClient(
            "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1",
            _mockContainerName
        );

        _repository = new ImageRepository(_client);
    }
    
    

    [Fact]
    public async Task Sucessful_Create_returns_url()
    {
        //Arrange
        var ExpectedResult = Status.Created;
        //Act
        var result = await _repository.CreateImageAsync("tester.jpg", "jpeg", Stream.Null);

        //Assert
        Assert.Equal(result.status, ExpectedResult);
        Assert.NotNull(result.uri);
    }
}