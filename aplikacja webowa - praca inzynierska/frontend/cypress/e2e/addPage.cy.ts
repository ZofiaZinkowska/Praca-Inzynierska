/// <reference types = 'cypress' />

import { plantRegisterHelper } from "../support/plantRegisterHelper"

const addPlant = '/Add';

describe('The „plant register” application', () => {
    before(() => {
        cy.visitPage(addPlant)
    })

    it('should contain proper page title', () => {
        plantRegisterHelper.verifyPageTitle('Dodawanie nowego wpisu');
    });
    
    it('should add plant to register', () => {
        plantRegisterHelper.searchPlantByKeywordAndSave();
        plantRegisterHelper.deletePlantFromRegister();
    });

    it('should add plant with id to register',() =>{
        cy.visitPage('/Add');
        plantRegisterHelper.searchPlantByKeyword();
        plantRegisterHelper.searchID();
        plantRegisterHelper.deletePlantFromRegister();
    });
})