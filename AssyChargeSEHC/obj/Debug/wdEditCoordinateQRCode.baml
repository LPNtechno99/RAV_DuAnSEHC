   M S B A M L   `   `   `  ����I  EAssyChargeSEHC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:AssyChargeSEHCAssyChargeSEHC  5   	   +    %AssyChargeSEHC.wdEditCoordinateQRCode   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:AssyChargeSEHC     5   	   -   5          � Title$  wdEditCoordinateQRCode��5   	   $	��300��6(   $	��300��65    � WindowStartupLocation$ CenterScreen=�6A    � 
ResizeMode$ NoResize=�5	   	    � 	Resources 5
         myCommandEsc �� Text��%(   &	        �� 5   
   $
 text��6/   5       J� InputBindings 5      �� 5   
   
 �� Key$	 Esc��6    �� Command# [  6    J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5        	   Command#	 [  5      6A   .��5      � 
 � RowDefinitions
 5   
   �� 5      �� 5      $��3* �6   �� 5      �� 5      $��3* �6   �� 5      .+�5   
   �� $��QRCode Coordinate��6   $��Center=�6,   $��Center=�6N   $��16(�5      $
��Bold"�6   
����������	#FFFF723D6C   �� 5   
   $��2��6   $��MaterialCode Coordinate��6   $��Center=�6?   $��Center=�6a   $��16(�5      $
��Bold"�6   
����������	#FFFF723D6C   � 5    
   $��1��6   
 5!      �� 5"      �� 5#      .+�5%      ��  �� Orientation$ 
Horizontal=�6   .+�5&      �� $��X Coor:��6    �� TextAlignment$ Center=�6+   $��Center=�6B   $��10 0 0 0q�5'      $��14(�6.   $
��Bold"�6<   �� 5(      -   5        $��
txtXCoorQR��5*      $��5 0 0 0q�5(       �� TextAlignment$ Right=�6+   $��0 0 2 0q�6A   $	��200��6S   $��30��5)      $��16(�6&   $
��Bold"�64   $��Center=�6F   $��45��6h   5*      5+      �� 5,      $��1��6   $ 
Horizontal=�6&   .+�5-      �� $��Y Coor:��6   $ Center=�6+   $��Center=�6B   $��10 0 0 0q�5.      $��14(�6.   $
��Bold"�6<   �� 5/      -   5        $��
txtYCoorQR��51      $��5 0 0 0q�5/      $ Right=�6+   $��0 0 2 0q�6A   $	��200��6S   $��30��50      $��16(�6&   $
��Bold"�64   $��Center=�6F   $	��220��6h   51      52      53      � 54   
   $��3��6   
 55      �� 56      �� 57      .+�59      �� $ 
Horizontal=�6   .+�5:      �� $��X Coor:��6   $ Center=�6+   $��Center=�6B   $��10 0 0 0q�5;      $��14(�6.   $
��Bold"�6<   �� 5<      -   5        $��txtXCoorMaterialCode��5>      $��5 0 0 0q�5<      $ Right=�6+   $��0 0 2 0q�6A   $	��200��6S   $��30��5=      $��16(�6&   $
��Bold"�64   $��Center=�6F   $��35��6h   5>      5?      �� 5@      $��1��6   $ 
Horizontal=�6&   .+�5A      �� $��Y Coor:��6   $ Center=�6+   $��Center=�6B   $��10 0 0 0q�5B      $��14(�6.   $
��Bold"�6<   �� 5C      -   5        $��txtYCoorMaterialCode��5E      $��5 0 0 0q�5C      $ Right=�6+   $��0 0 2 0q�6A   $	��200��6S   $��30��5D      $��16(�6&   $
��Bold"�64   $��Center=�6F   $��45��6h   5E      5F      5G      �� 5H   
   $��4��6   $��14(�6   $��Italic$�6+   $!��Press ESC for Save and Exit��6>   5I      5J      