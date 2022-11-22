/// <reference types="cypress" />

declare global {
    namespace Cypress {
        interface Chainable<Subject> {
            visitPage(url: string): Chainable<Subject>;
            visitWithRequest(url: string, request: string): Chainable<Subject>;
            verifyUrlContainsKeyword(keyword: string): Chainable<Subject>;
        }
    }
}

Cypress.Commands.add('visitPage', (url: string) => {
    cy.visit(url, {
    });
});

Cypress.Commands.add('visitWithRequest', (url: string, request: string) => {
    cy.intercept(request).as('request');
    cy.visit(url);
    cy.wait('@request');
});

Cypress.Commands.add('verifyUrlContainsKeyword', (keyword: string) => {
    cy.url().should('contain', keyword);
});

export {}
