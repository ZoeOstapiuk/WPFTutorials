   M S B A M L   `   `   `  ����  	MyWordPad    MyWordPad.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  MySpellChecker��5   	   $	��331��6    $	��508��6-    � WindowStartupLocation$ CenterScreen=�69    J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command$ ApplicationCommands.Open��5      6W     5   
   -   5        $ ApplicationCommands.Save��5      6W   .��5
      ]� .+�5   
   o� $	��Top=�6   $
��Left=�6$   
�������
���   �.@�5      m� $��_File��6   .@�5      m�  m� Command$ ApplicationCommands.Open��6   m� 5      $ ApplicationCommands.Save��6   �� 5      m� 5      -   5        $��_Exit��5      6a   5      m� 5      $��_Edit��6   .@�5      m� $ ApplicationCommands.Copy��6   m� 5      $ ApplicationCommands.Cut��6   m� 5      $ ApplicationCommands.Paste��6   5      m� 5      $��_Tools��6   .@�5      m� -   5        $��_Spelling Hints��5      5      5      5      c� 5   
   $	��Top=�6   .@�5      �� -   5        $
��Exit��5      6y   �� 5      �� 5      -   5        $��Check��5       � Cursor$
 Help��6�   6�   5       �� 5!   
   $��Bottom=�6   
�������.@�5"      �� .��5#      �� -   5        $��statBarText��5#      $��Ready��6/   5$      5%      � 5&   
   $
��Left=�6   
������� � ColumnDefinitions 5'      �� 5(      6%   �� 5)      6%   .+�5+      �� $��0��6   $��5��6+   
�������65   �� 5,      $��0��6   $��Stretch=�6)   .+�5-      �� -   5        $��lblSpellingInstructions��5-      $��14(�67   $��10,10, 0, 0q�6E   .��6Z   Spelling Hints6l   9� 5.      -	   5        $��expanderSpelling��5.      $��
Try these!��63   $��10,10,10,10q�6G   .��5/      �� -
   5        $��lblSpellingHints��5/      $��12(�64   50      51      �� 52      -   5        $��txtData��53      $��1��52       �� 	IsEnabled .  �� AcceptsReturn . $��14(�53   &   
����  �"	 �� VerticalScrollBarVisibility$
	 Auto=�54      $
 �� HorizontalScrollBarVisibility$

 Auto=�69   .��6^   56      57      58      59      