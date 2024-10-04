using GetYourPlaceApp.Models;

namespace GetYourPlaceApp.Components;

public partial class PropertyComponent : ContentView
{
	public PropertyComponent()
	{
		InitializeComponent();
	}

    #region Bindable Properties
    public static readonly BindableProperty PropertyProperty = BindableProperty.Create(
      nameof(Property),
      typeof(Property),
      typeof(PropertyComponent)
      );

    #endregion

    #region [Properties]
    public Property Property 
    { 
        get => (Property)this.GetValue(PropertyProperty);
        set => this.SetValue(PropertyProperty, value);
    }

    #endregion
}