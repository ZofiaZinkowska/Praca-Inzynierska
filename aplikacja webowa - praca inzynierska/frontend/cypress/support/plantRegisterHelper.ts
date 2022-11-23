/// <reference types="cypress" />

class PlantRegisterHelper {

    verifyPageTitle(pageTitle: string) {
        cy.get('.container .mb-0').should('have.text', pageTitle);
    }

    searchID(){
        cy.get('.input-group').within(() => {
            cy.get('.form-control').type('123456').should('have.value','')
        })
        cy.get('.btn-content .svg-inline--fa').click();
    }

    searchIDByKeyword(){
        cy.get('.input-group')
        .type('1234567').should('have.value','')
        cy.get('.btn-content .svg-inline--fa').click();
    }

    searchPlantByKeyword(){
        cy.get('.simple-typeahead-input')
        .type('Abutilon circinatum ((Willd. ex Spreng.) G.Don)').should('have.value','Abutilon circinatum ((Willd. ex Spreng.) G.Don)').click();
        cy.contains('.btn-content', 'Zapisz').click();
    }
}


export const plantRegisterHelper = new PlantRegisterHelper();