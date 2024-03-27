#Programmer(s): Tisdell, Matthew and Sims, Roddey
#Assignment: Data Analysis Project
#Course: CSCI/ISAT B104
 
#Defined libraries for usage throughout
import numpy as np
import matplotlib.pyplot as plt
import seaborn as sns
import pandas as pd
 
 
#User will be prompted to choose which kind of data plot they would like to view
print('\nPlease choose from the following data plots (type the number): Bar Graph (1), Heat Map (2), Box Plot (3)')
print('--------------------------------------------------')
data_plot = eval(input('Data Plot: '))
 
#While loop to reprompt the user until they give a valid input
while data_plot < 1 or 1<data_plot<2 or 2<data_plot<3 or data_plot>3:
    print('Invalid input, please try again.')
    data_plot = eval(input('Data Plot: '))
 
#If statement to determine which data plot to present
if data_plot == 1:
    #Bar Graph
    time = ['5 hours','4 hours','3 hours','2 hours','1 hour','less than hours']
 
    boys = [100,123,95,143,180,150]
 
    girls = [80,90,110,125,127,100]
 
 
    plt.bar(time, boys, label='boys',color='skyblue')
    plt.bar(time,girls, label='girls',bottom = boys)
 
    plt.xlabel('Time')
    plt.legend()
    plt.ylabel('Weight')
 
    plt.title('Electronics impact of weight')
 
    plt.show() 
elif data_plot ==2:
    
    excel_file = '/Users/roddeysims/Library/CloudStorage/OneDrive-UniversityofSouthCarolina/B104/Boys and girls on electronics.xlsx'
 
#Heatmap code
    df = pd.read_excel(excel_file)
    x1_clean = df.replace(np.NaN, 0)
    heatmap_data = x1_clean
    sns.heatmap(heatmap_data.corr(), cmap='Blues')
    plt.tight_layout()
    plt.show()
elif data_plot == 3:
   #Box Plot
   excel_file = '/Users/roddeysims/Library/CloudStorage/OneDrive-UniversityofSouthCarolina/B104/Boys and girls on electronics.xlsx'
   df = pd.read_excel(excel_file)
   df.shape
   df.head()
#Style/inserting data
   sns.set_style('whitegrid')
 
   sns.boxplot(x='q67',y='q80',data=df,color='skyblue')
 