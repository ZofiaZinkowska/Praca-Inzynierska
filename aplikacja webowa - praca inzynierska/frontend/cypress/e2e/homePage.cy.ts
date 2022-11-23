/// <reference types = 'cypress' />

import { plantRegisterHelper } from "../support/plantRegisterHelper";

const plantRegister = 'http://127.0.0.1:5173/';

describe('The „plant register” application', () => {
    before(() => {
        cy.visitPage(plantRegister)
    })

    it('should contain proper page title', () => {
        plantRegisterHelper.verifyPageTitle('Ewidencja roślin botanicznych');
    });
})