   M S B A M L   `   `   `  ����E  ATraceroute, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Traceroute
Traceroute  5   	       Traceroute.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Traceroute     5   	     � Title$  
Traceroute��5   	   $	��844��6   $	��595��6)   .��5	      � ��5
   
   �� 5       �� ImageSource$ /img/灰色砖墙.jpg��6   .+�5   
   � $	��340��6   $��Center=�6   $��
0,60,0,638q�68   .+�5      �� -   5         ��Name$ label_IPOrDomain��5      $��IP地址/域名��6.   $
��Left=�6@   $��0,21,0,0q�6[   $	��Top=�6m    J� RenderTransformOrigin$ -0.162,0.04&�6�   $��17(�6�   $��SimHei)�6�   
�������6�   �� 5      -   5         ��Name$ textBox_IPOrDomain��5      $��28��62   $��0,22,0,0q�6>    �� TextWrapping$
 Wrap=�6P   $	��Top=�6d   $	��212��6|   $��Right=�6�   $��16(�6�   �� 5      -   5         ��Name$ button_start��5      $��开 始��6+   $��Center=�69   $��0,73,0,0q�6V   $	��Top=�6h   $	��100��6�   $��30��6�   $��Microsoft YaHei)�6�   $��15(�6�   6�   5      �� 5   
   -   5         ��Name$ textBlock_start��5       �� TextAlignment$ Center=�6-   $��Center=�6D   $��	0,225,0,0q�6a   $
��Wrap=�6t   $	��Top=�6�   $��57��6�   $	��416��6�   $��18(�6�   
�������6�   � 5   
   $��Center=�6   $	��482��6,   $��	0,305,0,0q�69   $	��Top=�6L   $	��416��6d   .+�5      �� -   5        $ button_next��5      $��	下一步��6*   {�.  $��Center=�6K   $��	0,437,0,0q�6h   $	��Top=�6{   $��98��6�   $��30��6�   $��15(�6�   6�   �� 5      ���	#FFDDDDDC$��Center=�6)   $	��393��6F   
���   �$	��Top=�6b   $	��416��6z   � 5      -   5        	 �Name$	 Grid_result��5      $��Center=�6(   $	��393��6E   $	��Top=�6R   $	��416��6j   $��0q�6v   
 � ColumnDefinitions
 5      �� 5      $��70 �6'   �� 5      $��70 �6'   �� 5      $��70 �6'   �� 5      $��70 �6'   �� 5      $��* �6'   5      �� 5      -   5        $ textBlock_done��5      $ Center=�60   $��Center=�6G   $��	0,400,0,0q�6d   $
��Wrap=�6w   $	��Top=�6�   $	��162��6�   $��25��6�   $��18(�6�   
�������6�   5       5!      5"      