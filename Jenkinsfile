pipeline {
    agent any

    environment {
        PATH = "/usr/bin/dotnet:$PATH"
        PROJECT_NAME = "ProjetJenkins"
        DOCKER_IMAGE = "oumaimaazz/${PROJECT_NAME}" 
        DOCKER_CREDENTIALS = "dockerhub-credentials" // Remplacez par l'ID de vos credentials Docker Hub
        REPO_URL = "git@github.com:oumaimaazz23/ProjetJenkins.git"
    }

    stages {
        stage('Checkout') {
            steps {
                // Cloner le d�p�t GitHub
                git branch: 'main', url: "${REPO_URL}"
            }
        }

        stage('Restore Dependencies') {
            steps {
                // Restauration des d�pendances .NET
                sh "dotnet restore ${PROJECT_NAME}.sln"
            }
        }

        stage('Build') {
            steps {
                // Compilation du projet
                sh "dotnet build ${PROJECT_NAME}.sln --configuration Release"
            }
        }

        stage('Test') {
            steps {
                // Ex�cution des tests unitaires
                sh "dotnet test Tests/${PROJECT_NAME}_test.csproj --logger trx"
            }
        }

        stage('Build Docker Image') {
            steps {
                script {
                    // Construire l'image Docker
                    sh "docker build -t ${DOCKER_IMAGE}:latest ."
                }
            }
        }

        stage('Push Docker Image') {
            steps {
                script {
                    // Push de l'image Docker sur Docker Hub
                    withCredentials([usernamePassword(credentialsId: DOCKER_CREDENTIALS, usernameVariable: 'DOCKER_USERNAME', passwordVariable: 'DOCKER_PASSWORD')]) {
                        sh '''
                        echo $DOCKER_PASSWORD | docker login -u $DOCKER_USERNAME --password-stdin
                        docker push ${DOCKER_IMAGE}:latest
                        '''
                    }
                }
            }
        }
    }

    post {
        always {
            // Nettoyage apr�s le pipeline
            cleanWs()
        }
        success {
            echo "Pipeline termin� avec succ�s !"
        }
        failure {
            echo "Le pipeline a �chou�. V�rifiez les logs."
        }
    }
}
