   M S B A M L   `   `   `  ����D  @BlankApp2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:BlankApp2.ViewsBlankApp2.Views  5          BlankApp2.Views.TCPChat   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:BlankApp2.Views     5      _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      [ WPrism.DryIoc.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59T PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5	      !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  True5
      .��5      � .+�5   
   �� -   5         ��Name$ ChatRichTextBox��5       �� 
IsReadOnly$
 True��6/   $
��Left=�6A   " �� VerticalScrollBarVisibility$
 Auto=�6\   $	��200��6   $��
10,110,0,0q�6�   $	��Top=�6�   $	��224��6�   . �5      /� .T�5      J� .*�6   .*�5      �� $� ��6   5      5      5      �� 5   
   -   5        $��
DialogName��5      $
��Left=�6$   $��	10,65,0,0q�6?    �� TextWrapping$
 Wrap=�6R   $	��Top=�6�   $	��120��6�   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 	대화명��6�      
IsFloating$
 True��6�   ��6f   �� *
DialogName+ �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5   
   $��연결하기��6   $
��Left=�6    $��
180,54,0,0q�6;   $	��Top=�6O   ��6g   �� *
CmdConnect+�� 5   
   $��종료��6   $
��Left=�6   $��
180,88,0,0q�69   $	��Top=�6M   $��88��6e   ��6p   �� *CmdDisConnect+�� 5   
   -   5        $��txtIP��5      $
��Left=�6   $��	10,25,0,0q�6:   $
 Wrap=�6M   $	��Top=�6�   $	��120��6�   $ IP��6�   $
 True��6�   ��6a   �� *
ServerIP+$ PropertyChanged=�6�   �� 5   
   -   5        $��txtPort��5      $
��Left=�6!   $��
140,25,0,0q�6<   $
 Wrap=�6P   $	��Top=�6�   $��80��6�   $
 Port��6�   $
 True��6�   ��6d   �� *
ServerPort+$ PropertyChanged=�6�   �� 5   
   -   5        $��ChatData��5      $
��Left=�6"   $��
10,315,0,0q�6=   $
 Wrap=�6Q   $	��Top=�6�   $	��170��6�   ��6e   �� *SendMessage+$ PropertyChanged=�6�   �� 5   
   $��전송��6   $
��Left=�6   $��185,314,0,0q�69   $	��Top=�6N   $��65��6f   ��6q   �� *	CmdSend+5      5      