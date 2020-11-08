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

club_type = {
    '1W' : 'ONE_WOOD',
    '2W' : 'TWO_WOOD',
    '3W' : 'THREE_WOOD',
    '4W' : 'FOUR_WOOD',
    '5W' : 'FIVE_WOOD',
    '7W' : 'SEVEN_WOOD',
    '9W' : 'NINE_WOOD',
    '2H' : 'TWO_HYBRID',
    '3H' : 'THREE_HYBRID',
    '4H' : 'FOUR_HYBRID',
    '5H' : 'FIVE_HYBRID',
    '6H' : 'SIX_HYBRID',
    '7H' : 'SEVEN_HYBRID',
    '1I' : 'ONE_IRON',
    '2I' : 'TWO_IRON',
    '3I' : 'THREE_IRON',
    '4I' : 'FOUR_IRON',
    '5I' : 'FIVE_IRON',
    '6I' : 'SIX_IRON',
    '7I' : 'SEVEN_IRON',
    '8I' : 'EIGHT_IRON',
    '9I' : 'NINE_IRON',
    'PW' : 'PITCHING_WEDGE',
    'GW' : 'GAP_WEDGE',
    'SW' : 'SAND_WEDGE',
    'LW' : 'LOB_WEDGE',
    'FW' : 'FINAL_WEDGE',
    'P'  : 'PUTTER',
}

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
        coefficients = ', '.join([f'{str(coefficient)}f' for coefficient in model.coef_])
        print(f'{{ ClubType.{club_type[club]}, new Polynomial({model.intercept_}f, {coefficients}) }},')
    
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
