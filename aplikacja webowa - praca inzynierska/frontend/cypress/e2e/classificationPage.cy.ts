/// <reference types = 'cypress' />

import { plantRegisterHelper } from "../support/plantRegisterHelper";

const classification = '/Taxonomy';

describe('The „plant register” application', () => {
    before(() => {
        cy.visitPage(classification);
    });

    it('should contain proper page title', () => {
        plantRegisterHelper.verifyPageTitle('Klasyfikacja');
    });

    it('should search plant name by keyword in the classification', () => {
        plantRegisterHelper.searchPlantByKeyword();
    });

    it('should print the label', () => {
        plantRegisterHelper.prinPlantLabel();
    });
})