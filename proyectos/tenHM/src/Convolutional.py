#%%
from __future__ import division
from __future__ import print_function
from __future__ import unicode_literals
from __future__  import absolute_import


import tensorflow as tf
from tensorflow import keras


# Librerias de apoyo
import numpy as np
import matplotlib.pyplot as plt
import pickle

class Convolutional(object):

    def __init__(self):
        # Abre los archivos
        self.train_images = pickle.load(open('x.pickle', 'rb'))
        self.train_labels = pickle.load(open('y.pickle', 'rb'))

        # Se declara el modelo como una lista
        self.model = keras.Sequential([
            keras.layers.Flatten(input_shape=(50, 50,1)),
            keras.layers.Dense(128, activation=tf.nn.relu),
            keras.layers.Dense(6, activation=tf.nn.softmax)
        ])

        # Se compila el modelo
        self.model.compile(optimizer='adam',
              loss='sparse_categorical_crossentropy',
              metrics=['accuracy'])



    def training(self):

        # Se entrena el modelo con n iteraciones, 
        # Train_images es el arreglo donde gurdamos las imagenes en el tensor, 
        # labels es el arreglo de entrenamiento
        self.model.fit(self.train_images, self.train_labels, epochs=100)

        # Guardamos el modelo
        self.model.save('model.h5')

# %%
