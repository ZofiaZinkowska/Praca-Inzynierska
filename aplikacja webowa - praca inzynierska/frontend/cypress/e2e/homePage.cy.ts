/// <reference types = 'cypress' />

import { plantRegisterHelper } from "../support/plantRegisterHelper";

const plantRegister = '/';

describe('The „plant register” application', () => {
    before(() => {
        cy.visitPage(plantRegister)
    })

    it('should contain proper page title', () => {
        plantRegisterHelper.verifyPageTitle('Ewidencja roślin botanicznych');
    });
})