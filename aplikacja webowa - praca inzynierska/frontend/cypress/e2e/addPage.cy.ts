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
    
    it('', () => {
        plantRegisterHelper.searchIDByKeyword();
        plantRegisterHelper.searchPlantByKeyword();
    });
})