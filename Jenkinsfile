pipeline {
    agent any
    options {
        skipStagesAfterUnstable()
    }
    stages {
        stage('Build') {
            steps {
                echo 'hi ,in Build'
            }
        }
        stage('Test'){
            steps {
                echo 'hi , in test'
            }
        }
        stage('Deploy') {
            steps {
                echo 'hi , in deploy'
            }
        }
    }
} 

