/// <reference types="cypress" />

class PlantRegisterHelper {

    verifyPageTitle(pageTitle: string) {
        cy.get('.container .mb-0').should('have.text', pageTitle);
    }

    searchIDByKeyword(){
        cy.get('.input-group')
        .type('123456').should('have.value','')
        cy.get('.btn-content .svg-inline--fa').click();
    }

    searchPlantByKeyword(){
        cy.get('.simple-typeahead-input')
        .type('×Algastoloba (D.M.Cumming)').should('have.value','×Algastoloba (D.M.Cumming)').click();
        cy.contains('.btn-content', 'Zapisz').click();
    }
}


export const plantRegisterHelper = new PlantRegisterHelper();