   M S B A M L   `   `   `  ����  WpfApp8     KeyboardTrainer.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$/  )Клавиатурный тренажер��5   	   $	��400��6'   $	��600��64   .��5      � .+�5   
   ��  �� Orientation$ Vertical=�6   $��10q�6,   .+�5	      �� $/��)Клавиатурный тренажер��6   $��20(�65   $
��Bold"�6C    �� TextAlignment$ Center=�6U   �� 5      $ 
Horizontal=�6   $��5q�62   .+�5      �� $��Длина строки:��6   $��Center=�61   �� 5      -   5         ��Name$ SliderLength��5       � Minimum$ 10Q�6/    � Maximum$ 50Q�6<    � Value$ 20Q�6I   $	��100��6T   $��5q�6`   �� 5      -   5         ��Name$ CheckCaseSensitive��5      $'��!Учитывать регистр��67   $��5q�6S   5      �� 5      $ 
Horizontal=�6   $��5q�62   .+�5      �� -   5         ��Name$ BtnStart��5      $��Start��6+   $��75��6R   �� 5      -   5        $ BtnStop��5      $
��Stop��6*   $��75��6O   ${�False��6Z   �� 5      -   5        	 ��Name$	 	TextStats��5      $7��1Скорость: 0 зн/мин | Ошибки: 0��6/   $��10q�6U   5      �� 5      -   5        $	 
TextToType��5      $��18(�6,   $
��Bold"�6:   $ Center=�6L   $��10q�6c   �� 5      -   5        $	 	TextInput��5      $��18(�6+   $ Center=�69   $��10q�6P   
�������6\   5      A� 5   
   -	   5        
 A�Name$
 UniformGrid��5       A� Rows$ 4��6+    A� Columns$ 10��64   $��10,150,10,10q�6A   5      5       5!      