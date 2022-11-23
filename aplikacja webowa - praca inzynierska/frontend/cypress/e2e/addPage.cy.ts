/// <reference types = 'cypress' />

import { plantRegisterHelper } from "../support/plantRegisterHelper"

const addPlant = 'http://127.0.0.1:5173/Add';

describe('The „plant register” application', () => {
    before(() => {
        cy.visitPage(addPlant)
    })

    it('should contain proper page title', () => {
        plantRegisterHelper.verifyPageTitle('Dodawanie nowego wpisu');
    });
    
    it('', () => {
        plantRegisterHelper.searchIDByKeyword();
        plantRegisterHelper.searchPlantByKeyword();
    });
})