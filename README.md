Version        | Build status     | NuGet package
---------------|------------------|----------------|
Prerelease     | ![Build Status](https://dev.azure.com/rotorsoft/CollectionViewSource.Forms/_apis/build/status/CollectionViewSource.Forms%20staging?branchName=staging) | [![NuGet](https://img.shields.io/nuget/vpre/CollectionViewSource.Forms.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/CollectionViewSource.Forms)
Stable         | ![Build Status](https://dev.azure.com/rotorsoft/CollectionViewSource.Forms/_apis/build/status/CollectionViewSource.Forms%20staging?branchName=master) | [![NuGet](https://img.shields.io/nuget/v/CollectionViewSource.Forms.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/CollectionViewSource.Forms)

# CollectionViewSource.Forms
A CollectionViewSource implementation for Xamarin Forms that supports filtering, sorting and grouping.

## Quickstart
Add the CollectionView.Forms NuGet package to your Xamarin Forms project:
    
    Install-Package CollectionViewSource.Forms -IncludePrerelease
    
All existing classes and interfaces reside in a single namespace. To use them from C# code:

  ```csharp
  using Rotorsoft.Forms;
  ```
And to use them from XAML markup:
  
  ```xaml
  xmlns:rotorsoft="clr-namespace:Rotorsoft.Forms;assembly=CollectionViewSource.Forms"
  ```
To use a `CollectionViewSource` object from XAML, create it as a static resource at the page level and **remember to set the `BindingContext` explicitly** (unlike WPF or UWP, Xamarin Forms doesn't propagate the parent's binding context to a child static resource).

  ```xaml
  <ContentPage.Resources>
      <rotorsoft:CollectionViewSource
          x:Key="DataSource"
          BindingContext="{Binding Path=BindingContext, Source={x:Reference _page}, Mode=OneWay}" 
          Source="{Binding Items, Mode=OneWay}" />
  </ContentPage.Resources>  
  ```
  
  And then bind to its `View` property from either a `ListView` or `CollectionView`:
  
  ```xaml
  <CollectionView ItemsSource="{Binding View, Source={StaticResource DataSource}}">
  </CollectionView>
  ```
  
  All `CollectionViewSource` properties (except `Filter`, which can only be set using bindings or C# code) can be either set explicitly through XAML or through the use of bindings:
  
  ```xaml
  <rotorsoft:CollectionViewSource
      x:Key="XamlDataSource">
      <rotorsoft:CollectionViewSource.Source>
          <collections:List x:TypeArguments="x:String">
              <x:String>Lorem</x:String>
              <x:String>Ipsum</x:String>
              <x:String>Dolor</x:String>
              <x:String>Sit</x:String>
              <x:String>Amet</x:String>
          </collections:List>
      </rotorsoft:CollectionViewSource.Source>
      <rotorsoft:CollectionViewSource.SortDescriptions>
          <x:Array Type="{x:Type rotorsoft:SortDescription}">
              <rotorsoft:SortDescription Direction="Ascending" PropertyName="" />
          </x:Array>
      </rotorsoft:CollectionViewSource.SortDescriptions>
  </rotorsoft:CollectionViewSource>     
  ```
  
  ```xaml
  <rotorsoft:CollectionViewSource
      x:Key="BindingsDataSource"
      BindingContext="{Binding Path=BindingContext, Source={x:Reference _page}, Mode=OneWay}"
      Source="{Binding Items, Mode=OneWay}"
      Filter="{Binding Filter, Mode=OneWay}"
      SortDescriptions="{Binding SortDescriptions, Mode=OneWay}" />
  ```
