/// <reference types="cypress" />

class PlanRegisterHelper {

    verifyPageTitle(pageTitle: string) {
        cy.get('.container .mb-0').should('have.text', pageTitle);
    }
}


export const planRegisterHelper = new PlanRegisterHelper();