#%%
# Librerias de apoyo
import os
import cv2
import numpy as np
import random
import pickle

from matplotlib import pyplot as plt

class Preprocess:

    # Definimos nuestros atributos que usaremos
    def __init__(self):

            self.DATADIR = "data"
            self.CATEGORIES = ["Man", "Woman", "Other"]
            self.IMG_SIZE = 50
            self.training_data= []
            self.labels = []
            self.features = []

    # Cargamos las imagenes para que puedan leerse y clasificarse
    def load_training_data(self):
        for category in self.CATEGORIES:
            path = os.path.join(self.DATADIR, category)
            class_num = self.CATEGORIES.index(category)
            for img in os.listdir(path):
                try:
                    img_array = cv2.imread(os.path.join(path,img), cv2.IMREAD_GRAYSCALE)
                    new_array = cv2.resize(img_array,(self.IMG_SIZE, self.IMG_SIZE))
                    self.training_data.append([new_array,class_num])
                except Exception as e:
                    pass

    # Reacomodamos la imagen para que pueda analizarse
    def split_and_prepare(self):
        random.shuffle(self.training_data)
        for features, label in self.training_data:
            self.features.append(features)
            self.labels.append(label)
        self.features = np.array(self.features).reshape(-1, self.IMG_SIZE, self.IMG_SIZE,1)
        self.features = self.features/255.0

    # Creamos los pickles y los guardamos
    def write_out(self):
        pickle_out =open("y.pickle", "wb")
        pickle.dump(self.labels, pickle_out)
        pickle_out.close()

        pickle_out = open("x.pickle","wb")
        pickle.dump(self.features, pickle_out)
        pickle_out.close()

# %%
