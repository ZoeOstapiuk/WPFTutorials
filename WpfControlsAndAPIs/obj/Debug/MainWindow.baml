   M S B A M L   `   `   `  ����\  XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35fGclr-namespace:System.Windows.Annotations;assembly=PresentationFrameworkSystem.Windows.Annotations  5   	   M IWpfControlsAndAPIs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:WpfControlsAndAPIsWpfControlsAndAPIs 5   	   #   WpfControlsAndAPIs.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml      5   	   UaGclr-namespace:System.Windows.Annotations;assembly=PresentationFramework      5   	   4myCtrls clr-namespace:WpfControlsAndAPIs      5   	     � Title$  API And Controls��5   	   $	��350��6"   $��623.667��6/    J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5	   
   -   5           Command$ ApplicationCommands.Open��5	      6W     5
   
   -   5        $ ApplicationCommands.Save��5
      6W   .��5      � .+�5   
   �� -   5         ��Name$ myTabSystem��5      $��10,10,10,10q�6*   $	��Top=�6?   .@�5      �� $��Ink API��6   .��5      �� ���	#FFE5E5E5.+�5      c� -   5         c�Name$ 
inkToolBar��5      $
��Left=�62   $��35��6M   $	��580��6Y   .@�5      �� $
��Save��6!   $��ApplicationCommands.Save��60   �� 5      $
��Open��6!   $��ApplicationCommands.Open��60   �� 5      $	��0,2q�6!   $	��300��6.   ._�5      � .+�5   "   	� -   5         	�Name$ inkRadio��5   .   $��Center=�6@   $��	Ink Mode!��6[    	� 	GroupName$ InkMode��6o   $��	0,0,200,1q�6�    d� 	IsChecked$
 TrueY�6�   	� 5   "   -   5        $ 
eraseRadio��5   .   $��Center=�6B   $��Erase Mode!��6]   $ InkMode��6s   $��100,0,100,1q�6�   6�   	� 5   "   -   5        $ selectRadio��5   .   $��Center=�6C   $��Select Mode!��6^   $ InkMode��6u   $��	200,0,0,0q�6�   6�   5      5      �� 5      �� 5      -   5         ��Name$ comboColors��5   #   $	��100��68   .@�5      �� 	 �� Orientation$	 
Horizontal=�6)   .+�5   "   A� 
���  ��$��20��65   $��20��6A   �� 5   "   $��12(�6(   $��Center=�66   $��Center=�6S   $	��Red��6n   5       �� 5!      $	 
Horizontal=�6)   .+�5"   "   A� 
��� � �$��20��67   $��20��6C   �� 5#   "   $��12(�6(   $��Center=�66   $��Center=�6S   $��Green��6n   5$      �� 5%      $	 
Horizontal=�6)   .+�5&   "   A� 
����  �$��20��66   $��20��6B   �� 5'   "   $��12(�6(   $��Center=�66   $��Center=�6S   $
��Blue��6n   5(      5)      �� 5*      -	   5        $��Clear��5*   !   61   5+      �� 5,      -
   5        
 ��Name$
 myInkCanvas��5,       5-      5.      �� 5/      -   5         ��Name$ tabDocuments��5/      $��	Documents��6,   $��Bottom=�6?   $��20��6Z   .��50      �� ���	#FFE5E5E5.+�51      c� .@�52      �� -   5         ��Name$ 
btnSaveDoc��52   !   $��Stretch=�65   $��Stretch=�6S   $��75��6o   $��Save Doc��6z   �� 53      -   5        $ 
btnLoadDoc��53   !   $��Stretch=�65   $��Stretch=�6S   $��75��6o   $��Load Doc��6z   �� 54      
��� � �$��Add Sticky Note��65   $5��/a:AnnotationService.CreateTextStickyNoteCommand��6O   �� 55      
��� � �$��Delete Sticky Note��65   $2��,a:AnnotationService.DeleteStickyNotesCommand��6R   �� 56      
��� � �$��Highlight Text��65   $0��*a:AnnotationService.CreateHighlightCommand��6N   57      -� 58      -   5         -�Name$ myDocumentReader��58   )   $	��236��6C   .��59      /� .T�5:      �� 
��� ���
���   �.�5;   "   J� $��20(�6,   .*�6:   64Here are some fun facts about the WPF Documents API!5=   #   5>      �� 5?      -   5         ��Name$ listOfFunFacts��5?   #   J� 5@      -   5         J�Name$ paraBodyText��5@   (   5A      5B      5C      5D      �� 5E      -   5        $ tabDataBinding��5E      $��Data Binding��6.   .��5F      �� ���	#FFE5E5E5 � DataContext 64   ��  �� ElementName$
 mySB��.+�5G      �� $3��-Move the scroll bar to see the current value!��6   �� 5H      -   5         ��Name$
 mySB��5H   ,   $��20q�6     �� Orientation$ 
Horizontal=�6:   $��30��6S    � Minimum$ 1Q�6_    � Maximum$	 100Q�6k    � LargeChange$ 1Q�6y    � SmallChange$ 1Q�6�   �� 5I      -   5         ��Name$ 
lblSBThumb��5I      $��30��60   
����  �$��2q�6O   $��0��6c   �� 5J      $��20q�6   $��Just button��6)   $	��200��6?   $��50��6K   ��6W   ��  �� Path$ Value�5K      5L      �� 5M      -   5        $ tabDataGrid��5M      $��	Data Grid��6+   .��5N      �� .+�5O      &    System.Windows.Controls.DataGrid  -   5          Name$ gridInventory��5O      $	��288��66   5P      5Q      �� 5R      -   5        $ 	tabButton��5R      $��Button Template��6)   .��5S      �� .+�5T      �� -   5        $ myButton��5T      $	��100��6/   $	��100��6;      RoundButtonTempate#���   6_   5U      5V      5W      5X      5Y      