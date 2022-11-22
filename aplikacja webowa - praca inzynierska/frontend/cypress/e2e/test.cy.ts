/// <reference types = 'cypress' />

import { planRegisterHelper } from "../support/plantRegisterHelper"

const plantRegister = ''

describe('The „plant register” application', () => {
    before(() => {
        cy.visitPage(plantRegister)
    })

    it('should contain proper page title', () => {
        planRegisterHelper.verifyPageTitle('Ewidencja roślin botanicznych');
    })
})