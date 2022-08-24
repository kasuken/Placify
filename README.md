# Placify - yet another image placeholder saas

Placify is a SaaS image placeholder service. It allows you to create and manage image placeholders for your SaaS products. You can use it to create placeholder images for your products, and then use them in your application. You can also use it to create placeholder images for your marketing materials.

![](https://www.placify.xyz/api/draw/1920/768)

## How it works

It's very simple, you can add an img tag in your HTML page and it will be replaced with a placeholder image.

```html
<img src="https://www.placify.xyz/api/draw/1920/768>
```

### How the data is stored:

Placify saves the binary stream of the image directly in Redis. As a Key, the application creates a key formatted as width*height.

### How the data is accessed:

By the key the application retrieves the binary information and send it back as the API result.

### Performance Benchmarks

I did some tests with the solution and the results are as follows:

Generating a new image with cache: **200ms** (average)
Generating a new image without cache: **40ms** (average)

## How to run it locally?

### with Visual Studio 2022
Open the solution from the source code.
Set as Startup Projects the Api project and the Client project.

### Prerequisites

- .NET 6
- Azure Functions Tools
- Azure Redis Cache instance

## Deployment

The deployment process is automated by GitHub Actions.
The application is deployed to an Azure Static Web App.