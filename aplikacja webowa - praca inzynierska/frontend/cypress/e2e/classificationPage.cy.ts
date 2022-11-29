/// <reference types = 'cypress' />

import { plantRegisterHelper } from "../support/plantRegisterHelper";

const plantRegister = '/Taxonomy';

describe('The „plant register” application', () => {
    before(() => {
        cy.visitPage(plantRegister)
    })

    it('should contain proper page title', () => {
        plantRegisterHelper.verifyPageTitle('Klasyfikacja');
    });

    it('should search id by keyword', () => {
        plantRegisterHelper.searchID();
    });
})