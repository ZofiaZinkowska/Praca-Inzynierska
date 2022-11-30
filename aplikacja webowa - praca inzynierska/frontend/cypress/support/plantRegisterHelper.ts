/// <reference types="cypress" />

class PlantRegisterHelper {

    verifyPageTitle(pageTitle: string) {
        cy.get('.container .mb-0').should('have.text', pageTitle);
    }

    searchID(){
        cy.get('.input-group')
        .type('test').should('have.value','');
        cy.contains('.btn-content', 'Zapisz').click();
    }

    searchIDByKeyword(){
        cy.get('.input-group')
        .type('test').should('have.value','');
        cy.get('button .btn-content .svg-inline--fa').click();
        cy.contains('.btn-content', 'Zapisz').click();
    }

    searchPlantByKeyword(){
        cy.get('.simple-typeahead-input')
        .type('blob').should('have.value','blob').click();
        cy.get('.simple-typeahead-list-item').click();
    }

    searchPlantByKeywordAndSave(){
        cy.get('.simple-typeahead-input')
        .type('blob').should('have.value','blob').click();
        cy.get('.simple-typeahead-list-item').click();
        cy.contains('.btn-content', 'Zapisz').click();
    }

    deletePlantFromRegister(){
        cy.visitPage('/List');
        cy.contains('tr', 'Abelmoschus sublobatus (C.Presl)').trigger('mouseover').within(()=>{
            cy.get('button[title="Usuń"]').click();
        });
    }

    prinPlantLabel(){
        cy.contains('button','Drukuj etykietę').click();
    }
}


export const plantRegisterHelper = new PlantRegisterHelper();