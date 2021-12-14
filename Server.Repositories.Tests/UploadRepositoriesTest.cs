using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Storage;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SETraining.Server.Repositories;
using SETraining.Shared;
using Xunit;

namespace Server.Repositories.Tests;

public class UploadRepositoriesTest : IDisposable
{
    private BlobServiceClient _serviceClient;
    private BlobContainerClient _containerClient;
    private UploadRepository _repository;
    private string _mockUserName;
    private string _mockContainerName;
    
    public UploadRepositoriesTest()
    {
        //Account name
        _mockUserName = "devstoreaccount1";
        _mockContainerName = "images";
        
        //Using Azurite from Docker Image to mock Azure Blob Storage
        _serviceClient = new BlobServiceClient(
            "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;");
        
        _containerClient = _serviceClient.CreateBlobContainer("imagetest");
        
        _repository = new UploadRepository(_containerClient);
    }

    [Fact]
    public async Task CreateUploadAsync_returns_status_uri()
    {
        //Arrange
        var ExpectedResult = Status.Created;
        
        //Act
        var result = await _repository.CreateUploadAsync("tester.jpg", "jpeg",  new MemoryStream());

        //Assert
        Assert.NotNull(result.uri);
    }
    
    [Fact]
    public async Task CreateUploadAsync_returns_status_Created()
    {
        //Arrange
        var ExpectedResult = Status.Created;
        
        //Act
        var result = await _repository.CreateUploadAsync("tester.jpg", "jpeg",  new MemoryStream());

        //Assert
        Assert.Equal(result.status, ExpectedResult); 
    }
    
    [Fact]
    public async Task Uploaded_has_correct_media_type()
    {
        //Arrange
        var ExpectedResult = Status.Created;
        
        //Act
        var result = await _repository.CreateUploadAsync("tester.jpg", "mp4",  new MemoryStream());
        
        //Assert
        Assert.Equal(result.status, ExpectedResult); 
    }
    
    //TODO: Lav test med data fra stream

    public void Dispose()
    {
         _containerClient.Delete();
    }
}