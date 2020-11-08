'''
fit_power_curve.py

Fit club power curves using polynomial regression.
'''

import numpy as np
import pandas as pd
from sklearn.preprocessing import PolynomialFeatures
from sklearn.pipeline import make_pipeline
from sklearn.linear_model import LinearRegression
import matplotlib.pyplot as plt

def get_input_and_target(df, club):
    X = df[club]
    valid_indices = ~np.isnan(X)
    X = X[valid_indices].to_frame()
    y = df['Power'][valid_indices]
    return (X, y)

def polynomial_regression(X, y, degree):
    polynomial_regression = make_pipeline(PolynomialFeatures(degree), LinearRegression())
    return polynomial_regression.fit(X, y)

def main():
    # Read data
    cols = ['Power', '1W', '3W', '5W', '3I', '4I', '5I',
            '6I', '7I', '8I', '9I', 'PW', 'SW', 'LW', 'P']
    clubs = cols[1:]
    df = pd.read_csv('power_data.csv', names=cols, header=0)

    data = [get_input_and_target(df, club) for club in clubs]
    
    # Create models
    regressions = [polynomial_regression(X, y, degree=5) for (X, y) in data]

    # Print models
    for (club, regression) in zip(clubs, regressions):   
        model = regression.named_steps['linearregression']
        print(f'{club}: {model.intercept_}, {model.coef_}')
    
    # Create plot
    plt.figure()
    plt.title('Power Curve')
    plt.xlabel('Proportion of max distance')
    plt.ylabel('Proportion of max force')
    for (regression, datum) in zip(regressions, data):
        X = datum[0]
        y = datum[1]
        plt.scatter(X, y)
        plt.plot(X, regression.predict(X))
    plt.show()

if __name__ == '__main__':
    main()
