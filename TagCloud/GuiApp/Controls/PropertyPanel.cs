﻿using TagCloud;

namespace GuiApp.Controls;

public class PropertyPanel : TableLayoutPanel
{
    private ApplicationProperties properties;
    
    public PropertyPanel(ApplicationProperties properties)
    {
        this.properties = properties;
        BorderStyle = BorderStyle.FixedSingle;
        ColumnCount = 1;
        AddControls();
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        SetDefaultSetAppProperties();
        BindPropertiesToAppProperties();
    }

    private void AddControls()
    {
        Controls.Add(font);
        minFontSize.Control.Minimum = 0;
        minFontSize.Control.Maximum = 1000;
        Controls.Add(minFontSize);
        maxFontSize.Control.Minimum = 0;
        maxFontSize.Control.Maximum = 1000;
        Controls.Add(maxFontSize);
        Controls.Add(imageSizeProperties);
        
        Controls.Add(backgroundColor);
        Controls.Add(foregroundColor);

        density.Control.DecimalPlaces = 2;
        density.Control.Increment = (decimal)0.01;
        Controls.Add(density);
        
        openFileButton.Anchor = AnchorStyles.Right;
        Controls.Add(openFileButton);
        
        Controls.Add(excludedWords);
        
        renderButton.Anchor = AnchorStyles.Bottom;
        renderButton.Dock = DockStyle.Bottom;
        Controls.Add(renderButton);
        Controls.Add(saveButton);
    }
    
    private void SetDefaultSetAppProperties()
    {
        font.Control.SelectedText = properties.FontProperties.Family.Name;
        minFontSize.Control.Value = (decimal)properties.FontProperties.MinSize;
        maxFontSize.Control.Value = (decimal)properties.FontProperties.MaxSize;
        imageSizeProperties.Width = properties.SizeProperties.ImageSize.Width;
        imageSizeProperties.Height = properties.SizeProperties.ImageSize.Height;
        backgroundColor.Color = properties.Palette.Background;
        foregroundColor.Color = properties.Palette.Foreground;
        density.Control.Value = (decimal)properties.CloudProperties.Density;
    }
    
    private void BindPropertiesToAppProperties()
    {
        font.Control.SelectedValueChanged += OnFontChanged;
        minFontSize.Control.ValueChanged += OnMinFontSizeChanged;
        maxFontSize.Control.ValueChanged += OnMaxFontSizeChanged;
        imageSizeProperties.ValueChanged += OnImageSizePropertiesChanged;
        backgroundColor.ColorChanged += OnBackgroundColorChanged;
        foregroundColor.ColorChanged += OnForegroundColorChanged;
        openFileButton.FileChanged += OnFileChanged;
        density.Control.ValueChanged += OnDensityChanged;
        excludedWords.ExcludedWordsChanged += OnExcludedExcludedWordsChanged;
    }
    
    protected override void Dispose(bool disposing)
    {
        font.Control.SelectedValueChanged -= OnFontChanged;
        minFontSize.Control.ValueChanged -= OnMinFontSizeChanged;
        maxFontSize.Control.ValueChanged -= OnMaxFontSizeChanged;
        imageSizeProperties.ValueChanged -= OnImageSizePropertiesChanged;
        backgroundColor.ColorChanged -= OnBackgroundColorChanged;
        foregroundColor.ColorChanged -= OnForegroundColorChanged;
        openFileButton.FileChanged -= OnFileChanged;
        density.Control.ValueChanged -= OnDensityChanged;
        excludedWords.ExcludedWordsChanged -= OnExcludedExcludedWordsChanged;
        base.Dispose(disposing);
    }

    private ControlWithDescription<FontComboBox> font = new (new FontComboBox(), "Font");

    private void OnFontChanged(object? sender, EventArgs args)
    {
        properties.FontProperties.Family = new FontFamily((string)font.Control.SelectedItem);
    }

    private ControlWithDescription<NumericUpDown> minFontSize = new (new NumericUpDown(), "Minimum font size");
    
    private void OnMinFontSizeChanged(object? sender, EventArgs args)
    {
        properties.FontProperties.MinSize = (float)minFontSize.Control.Value;
    }
    
    private ControlWithDescription<NumericUpDown> maxFontSize = new (new NumericUpDown(), "Maximum font size");
    
    private void OnMaxFontSizeChanged(object? sender, EventArgs args)
    {
        properties.FontProperties.MaxSize = (float)maxFontSize.Control.Value;
    }
    
    private ImageSizeProperties imageSizeProperties = new(new Size(1280, 720));
    
    private void OnImageSizePropertiesChanged(object? sender, EventArgs args)
    {
        properties.SizeProperties.ImageSize = new Size(imageSizeProperties.Width, imageSizeProperties.Height);
    }
    
    private ColorProperties backgroundColor = new ("Background color");
    private void OnBackgroundColorChanged(object? sender, EventArgs args) => properties.Palette.Background = backgroundColor.Color;
    
    private ColorProperties foregroundColor = new ("Foreground color");
    private void OnForegroundColorChanged(object? sender, EventArgs args) => properties.Palette.Foreground = foregroundColor.Color;
    
    private OpenFileButton openFileButton = new ();
    private void OnFileChanged(object? sender, EventArgs args)
    {
        properties.Path = openFileButton.File;
        renderButton.IsRenderAvailable = true;
    }

    private ExcludedWords excludedWords = new ();
    private void OnExcludedExcludedWordsChanged(object? sender, EventArgs args)
    {
        properties.CloudProperties.ExcludedWords = excludedWords.Text.Split(' ', '\n', StringSplitOptions.RemoveEmptyEntries).ToList();
    }
    
    private ControlWithDescription<NumericUpDown> density = new (new NumericUpDown(), "Density");
    private void OnDensityChanged(object? sender, EventArgs args)
    {
        properties.CloudProperties.Density = (double)density.Control.Value;
    }
    
    private RenderButton renderButton = new ();
    private SaveButton saveButton = new ();
}