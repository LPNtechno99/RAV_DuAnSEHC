   M S B A M L   `   `   `  ����I  EAssyChargeSEHC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:AssyChargeSEHCAssyChargeSEHC  5   	   "    AssyChargeSEHC.wdCheckQRCode   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:AssyChargeSEHC     5   	     � Title$  wdCheckQRCode��5   	   $	��450��6   $
��1450��6,    � WindowStartupLocation$ CenterScreen=�69    � 
ResizeMode$ NoResize=�5	   	    � 	Resources 5
         myCommandEsc �� Text��%(   &	        �� 5   
   $
 text��6/   5       J� InputBindings 5      �� 5   
   
 �� Key$	 Esc��6    �� Command# [  6    J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5        	   Command#	 [  5      6A   .��5      � 
 � RowDefinitions
 5   
   �� 5      $��1* �6   �� 5      $��2* �6   .+�5   
   �� -   5         ��Name$ textboxData��5       �� TextWrapping$
 Wrap=�6'   $��18(�6;   $
��Bold"�6I   $�� ��6[   $��10q�6c   5      � 5   
   $��1��6   
 5      �� 5      $��* �6    �� 5      $��5* �6    .+�5      �� $��Result��6   $��Center=�6%   $
��Left=�6G   $��25(�5       ���	#FF0F258B
�������69   �� 5!      -   5        $��lbResult��5#   8   $��1��5!      $	��150(�6!   $
��Bold"�60   
�������$��Center=�5"      $��Center=�62   
���   �$��1q�5#   $   68   5$      5%      5&      