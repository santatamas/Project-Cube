   M S B A M L   `   `   `  ����L  HPixelMatrixEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:PixelMatrixEditor.ViewsPixelMatrixEditor.Views  5   	   K*clr-namespace:PixelMatrixEditor.ConvertersPixelMatrixEditor.Converters  5   	   (    "PixelMatrixEditor.Views.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7views%clr-namespace:PixelMatrixEditor.Views     5   	   A
converters*clr-namespace:PixelMatrixEditor.Converters     5   	     � Title$  Pixel Matrix Editor v0.2��5	   	   $	��800��6*   $	��800��67    � SizeToContent$ Manual=�6C    � 
ResizeMode$ 	CanResize=�6Z    � 	Resources 5
      <   6PixelMatrixEditor.Converters.ColorDepthToBoolConverter   ColorDepthToBoolConverter7   1PixelMatrixEditor.Converters.ByteToColorConverter  ByteToColorConverter��%:   &	        &	        5   
   6/     5   
   6*   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $
��auto �6   �� 5      $
��auto �6   .+�5   
   o� $��0��6   .@�5      m� $
��File��6   .@�5      m� $	��New��6    m� Command 6(   ��  �� Path$ 
NewCommand�6N   m� 5      $��Open...��6    6,   �� $ OpenCommand�6S   m� 5      $
��Save��6    6)   �� $ SaveCommand�6P   m� 5      $��	SaveAs...��6    6.   �� $ SaveAsCommand�6W   �� 5      6   m� 5      $��Close��6    6*   �� $ CloseCommand�6R   5      5       `� 5"   
   $��1��6   .�5#      c� .@�5$      �� .��6   Brush size:6%   �� 5%      -   5         ��Name$ AreaSizeComboBox��5%      $��40��65   ��6C   �� $ AreaSize� �� SelectedItem 6i   �� $ SelectedAreaSize��� 5&      6   �� 5'      .��6   Shade:6    �� 5(      -   5        $ ShadeComboBox��5(      $��40��62   ��5)      �� $ AvaliableShadeLevels� 5*      �� $ SelectedShadeLevel�{�5+      �� $ Animation.ColorDepth�	 �� 	Converter#	 [  ��5,      �� 5-      .R�5.      �� $��15��6(   $��15��63   ��6?   �� #	 [ 6�   5/      51      52      53      -   'PixelMatrixEditor.Views.PixelMatrixView  55   
   -   5        
  Name$
 
MatrixView��55       $��2��64   $��Center=�6A   $��Center=�6^      AreaSize 56       �� $ SelectedValue� �� ElementName$ AreaSizeComboBox��   MatrixSource 57       �� $ CurrentMatrix�   RenderDepth 58       �� $ Animation.ColorDepth�   
ShadeLevel 59       �� *SelectedShadeLevel++   %PixelMatrixEditor.Views.AnimationView  5;   
   $��3��6   ��6+   ��  �� RelativeSource ��  �� AncestorType" � �� Mode$ FindAncestor=�$ ActualWidth� � DataContext 6�   �� �� 5<   
   $��4��6   .@�5=      �� ��6   �� *ActiveStatusMessage+5>      5?      5@      